using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Repos
{
    internal class UserRepo :Repos, IUser<User>
    {


public User addUser(User user)
    {
        try
        {
            db.Users.Add(user);
            db.SaveChanges();
            return user;
        }
        catch (DbEntityValidationException ex)
        {
            var sb = new StringBuilder();

            foreach (var entityValidationErrors in ex.EntityValidationErrors)
            {
                sb.AppendLine($"Entity of type {entityValidationErrors.Entry.Entity.GetType().Name} in state {entityValidationErrors.Entry.State} has validation errors:");

                foreach (var validationError in entityValidationErrors.ValidationErrors)
                {
                    sb.AppendLine($"- Property: {validationError.PropertyName}, Error: {validationError.ErrorMessage}");
                }
            }

            // Throw new exception with detailed info
            throw new DbEntityValidationException(sb.ToString(), ex);
        }
    }

}
}
