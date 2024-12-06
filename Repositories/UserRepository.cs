﻿using FinancesApi.Data;
using FinancesApi.Models;
using FinancesApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinancesApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FinancesApiDbContext _context;

        public UserRepository(FinancesApiDbContext context)
        {   
            _context = context;
        }

        public async Task<List<UserModel>> FindAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<UserModel> FindByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null) throw new Exception("Não foi possível achar o usuário.");

            return user;
        }

        public async Task<UserModel> FindById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null) throw new Exception("Não foi possível achar o usuário.");

            return user;
        }

        public async Task<UserModel> Insert(UserModel user)
        {
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            user.SetPasswordHash();
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<UserModel> Update(UserModel user, int id)
        {
            var updatedUser = await FindById(id);

            if (updatedUser == null) throw new Exception("Usuário não encontrado");

            updatedUser.Name = user.Name;
            updatedUser.Email = user.Email;
            updatedUser.UpdatedAt = DateTime.Now;

            _context.Users.Update(updatedUser);
            await _context.SaveChangesAsync();
            return updatedUser;
        }

        public async Task<UserModel> Delete(int id)
        {
            var user = await FindById(id);

            if (user == null) throw new Exception("Usuário não encontrado");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;

        }
    }
}
