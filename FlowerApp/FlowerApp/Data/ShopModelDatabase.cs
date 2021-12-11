using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using FlowerApp.Models;
using System.Threading.Tasks;
using System.Linq;

namespace FlowerApp.Data
{
    public class ShopModelDatabase
    {
        public static SQLiteAsyncConnection Database; //was originally not public

        public static readonly AsyncLazy<ShopModelDatabase> Instance = new AsyncLazy<ShopModelDatabase>(async () =>
        {
            var instance = new ShopModelDatabase();
            int delete_user = await Database.DropTableAsync<Users>();
            int delete_admin = await Database.DropTableAsync<Admins>();
            int delete_flower = await Database.DropTableAsync<Flowers>();
            int delete_arrangement = await Database.DropTableAsync<Arrangements>();
            int delete_flowerarrangement = await Database.DropTableAsync<FlowerArrangements>();
            int delete_cart = await Database.DropTableAsync<Carts>();
            int delete_order = await Database.DropTableAsync<Orders>();

            //await DeleteAll();
            CreateTableResult result_user = await Database.CreateTableAsync<Users>();
            CreateTableResult result_admin = await Database.CreateTableAsync<Admins>();
            CreateTableResult result_flower = await Database.CreateTableAsync<Flowers>();
            CreateTableResult result_arrangement = await Database.CreateTableAsync<Arrangements>();
            CreateTableResult result_flowerarrangement = await Database.CreateTableAsync<FlowerArrangements>();
            CreateTableResult result_cart = await Database.CreateTableAsync<Carts>();
            CreateTableResult result_order = await Database.CreateTableAsync<Orders>();
            return instance;
        });

        public ShopModelDatabase()
        {
            Database = new SQLiteAsyncConnection(DBConstants.DatabasePath, DBConstants.Flags);
        }

        public Task<int> DeleteAll()
        {
            return Database.DeleteAllAsync<Users>();
        }

        
        /*
        public Task<Users> GetUserAsync()
        {   
            return Database.Table<Users>().ToListAsync();
        }*/

        /*
        public Task<Users> GetUserAsync(string username)
        {
            return Database.Table<Users>()
                .Where(i => i.UserUsername == username)
                .FirstOrDefaultAsync();
        }
        */

        public bool CheckIfUserUsernameorEmailTaken(Users user)
        {
            var sql_command= $"SELECT count(*) FROM Users where(UserUsername = '{user.UserUsername}' OR UserEmail = '{user.UserEmail}')";
            //var sql_command2 = $"SELECT count(*) FROM Users(Userusername = '{user.UserUsername}' AND state = '{user.UserEmail}')";
            //var username_email_count = await Database.QueryAsync<Users>(sql_command);
            var username_email_count = Database.ExecuteScalarAsync<int>(sql_command);

            if (username_email_count.Result == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
            //List<Users> result = username_email_count.ToList();
            //return username_email_count.ToList();
        }

        public bool CheckIfAdminUsernameorEmailTaken(Admins admin)
        {
            var sql_command = $"SELECT count(*) FROM Users where(UserUsername = '{admin.AdminUsername}' OR UserEmail = '{admin.AdminEmail}')";
            //var sql_command2 = $"SELECT count(*) FROM Users(Userusername = '{user.UserUsername}' AND state = '{user.UserEmail}')";
            //var username_email_count = await Database.QueryAsync<Users>(sql_command);
            var admin_email_count = Database.ExecuteScalarAsync<int>(sql_command);

            if ((int)admin_email_count.Result == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public Task<List<Users>> GetAllUsersAsync()
        {
            return Database.Table<Users>().ToListAsync();
        }

        /*
        public Task<List<TodoItem>> GetItemsNotDoneAsync()
        {
            return Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }
        */

        public Task<Users> GetUserAsync(string username)
        {
            return Database.Table<Users>().Where(i => i.UserUsername == username).FirstOrDefaultAsync();
        }

        public Task<Admins> GetAdminAsync(string username)
        {
            return Database.Table<Admins>().Where(i => i.AdminUsername == username).FirstOrDefaultAsync();
        }

        public Task<int> SaveUserAsync(Users user)
        {
            if (user.UserID != 0)
            {
                return Database.UpdateAsync(user);
            }
            else
            {
                return Database.InsertAsync(user);
            }
        }

        public Task<int> SaveAdminAsync(Admins admin)
        {
            if (admin.AdminID != 0)
            {
                return Database.UpdateAsync(admin);
            }
            else
            {
                return Database.InsertAsync(admin);
            }
        }

        /*
        public Task<int> DeleteItemAsync(TodoItem item)
        {
            return Database.DeleteAsync(item);
        }
        */
    }
}
