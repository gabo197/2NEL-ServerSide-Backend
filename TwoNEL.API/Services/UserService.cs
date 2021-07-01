using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;
using TwoNEL.API.Domain.Persistence.Repositories;
using TwoNEL.API.Domain.Services;
using TwoNEL.API.Domain.Services.Communications;
using TwoNEL.API.Settings;

namespace TwoNEL.API.Services
{
    public class UserService : IUserService
    {
        private AppSettings appSettings;
        private readonly IUserRepository userRepository;
        private readonly IFavoriteProfileRepository favoriteProfileRepository;
        private readonly IUnitOfWork unitOfWork;

        public UserService(IOptions<AppSettings> appSettings, IUserRepository userRepository, IUnitOfWork unitOfWork, IFavoriteProfileRepository favoriteProfileRepository)
        {
            this.appSettings = appSettings.Value;
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
            this.favoriteProfileRepository = favoriteProfileRepository;
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(14),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<AuthenticationResponse> Authenticate(AuthenticationRequest request)
        {
            var users = await userRepository.ListAsync();
            var user = users.SingleOrDefault(x => x.Email == request.Email
            && x.Password == request.Password);

            if (user == null) return null;

            var token = GenerateJwtToken(user);
            return new AuthenticationResponse(user, token);
        }

        public async Task<UserResponse> DeleteAsync(int id)
        {
            var existingUser = await userRepository.FindById(id);

            if (existingUser == null)
                return new UserResponse("User not found");

            try
            {
                userRepository.Remove(existingUser);
                await unitOfWork.CompleteAsync();

                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error ocurred while deleting the user: {ex.Message}");
            }
        }

        public async Task<UserResponse> GetByIdAsync(int id)
        {
            var existingUser = await userRepository.FindById(id);

            if (existingUser == null)
                return new UserResponse("User not found");
            return new UserResponse(existingUser);
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await userRepository.ListAsync();
        }

        public async Task<IEnumerable<Profile>> ListByFavoriteIdAsync(int favoriteId)
        {
            var favoriteProfiles = await favoriteProfileRepository.ListByFavoriteIdAsync(favoriteId);
            var profiles = favoriteProfiles.Select(st => st.Favorite).ToList();
            return profiles;
        }

        public async Task<IEnumerable<Profile>> ListByUserIdAsync(int userId)
        {
            var favoriteProfiles = await favoriteProfileRepository.ListByUserIdAsync(userId);
            var profiles = favoriteProfiles.Select(st => st.Profile).ToList();
            return profiles;
        }

        public async Task<UserResponse> SaveAsync(User user)
        {
            try
            {
                await userRepository.AddAsync(user);
                await unitOfWork.CompleteAsync();

                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error ocurred while saving the user: {ex.Message}");
            }
        }

        public async Task<UserResponse> UpdateAsync(int id, User user)
        {
            var existingUser = await userRepository.FindById(id);

            if (existingUser == null)
                return new UserResponse("User not found");

            existingUser.Email = user.Email;
            existingUser.Password = user.Password;

            try
            {
                userRepository.Update(existingUser);
                await unitOfWork.CompleteAsync();

                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error ocurred while updating the user: {ex.Message}");
            }
        }
    }
}
