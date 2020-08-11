using AutoMapper;
using SimpleCloudStorageServer.Dto;
using SimpleCloudStorageServer.Helper;
using SimpleCloudStorageServer.Helper.Exceptions;
using SimpleCloudStorageServer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCloudStorageServer.Service
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IJwtTokenService _jwtTokenService;

        public UserService(IMapper mapper, IUserRepository userRepository, IPasswordService passwordService, IFileService fileService, IJwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _mapper = mapper;
            _fileService = fileService;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<UserForRegisterResultDto> CreateUser(UserForRegisterDto detailsDto)
        {
            var user = await _userRepository.GetUserByUsername(detailsDto.Username);

            if (user != null)
            {
                throw new RegisterException("Such user does exists");
            }

            while (true)
            {
                var userDirectory = RandomStringUtils.GenerateRandomString();

                var dirUser = await _userRepository.GetUserByMainDirectory(userDirectory);

                if (dirUser == null) 
                {
                    _fileService.CreateDirectory(userDirectory);

                    user.MainDirectory = userDirectory;

                    break;
                }
            }

            string apiKey;
            string appId;

            while (true)
            {
                appId = RandomStringUtils.GenerateRandomString();

                var dirUser = await _userRepository.GetUserByAppId(appId);

                if (dirUser == null)
                {
                    apiKey = RandomStringUtils.GenerateApiKey();

                    _passwordService.CreatePasswordHash(apiKey, out byte[] apiKeyHash, out byte[] apiKeySalt);

                    user.AppId = appId;
                    user.ApiKeyHash = apiKeyHash;
                    user.ApiKeySalt = apiKeySalt;

                    break;
                }
            }

            _passwordService.CreatePasswordHash(detailsDto.Password, out byte[] hash, out byte[] salt);

            user.Username = detailsDto.Username;
            user.PasswordHash = hash;
            user.PasswordSalt = salt;
            user.CreatedAt = DateTime.UtcNow;

            await _userRepository.AddAsync(user);

            await _userRepository.SaveChanges();

            var dto = _mapper.Map<UserForRegisterResultDto>(user);

            dto.ApiKey = apiKey;
            dto.AppId = appId;

            return dto;
        }

        public async Task<UserForDeleteDto> ApplyDeleteActionToUser(long userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            
            if (user == null)
            {
                throw new BusinessException("Could not delete user");
            }

            user.DeletionAt = DateTime.UtcNow.AddDays(2);

            await _userRepository.SaveChanges();

            return _mapper.Map<UserForDeleteDto>(user);
        }

        public async Task<UserForDetailsDto> GetUser(long userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException("User has not been found");
            }

            return _mapper.Map<UserForDetailsDto>(user);
        }

        public async Task<LoginResultDto> Login(LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByUsername(loginDto.Username);

            if (user == null)
            {
                throw new NotFoundException("Wrong username or password");
            }

            if (!_passwordService.VerifyPassword(loginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new NotFoundException("Wrong username or password");
            }

            var loginResult = new LoginResultDto
            {
                AccessToken = _jwtTokenService.GenerateAccessToken(user, DateTime.UtcNow.AddMinutes(15)),
                RefreshToken = _jwtTokenService.GenerateAccessToken(user, DateTime.UtcNow.AddDays(1))
            };

            return loginResult;
        }
    }
}
