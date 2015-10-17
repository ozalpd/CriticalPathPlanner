using OzzIdentity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriticalPath.Data
{
    public partial class CriticalPathContext
    {
        //TODO: Create stored procedures to save entities and changes below value to true
        //then update functions will not need to ask original values from db.
        bool saveWithStoredProcs = false;


        /// <summary>
        /// Does not save any data if SessionInfo is not provided, so
        /// use SaveChanges(ISessionData sessionInfo) instead of this
        /// </summary>
        /// <returns>The number of objects written to the underlying database, if works :-)</returns>
        public override int SaveChanges()
        {
            //return base.SaveChanges();
            throw new Exception("Need session data!");
        }

        /// <summary>
        /// Does not save any data if SessionInfo is not provided, so
        /// use SaveChanges(ISessionData sessionInfo) instead of this
        /// </summary>
        /// <returns>The number of objects written to the underlying database, if works :-)</returns>
        public override Task<int> SaveChangesAsync()
        {
            //return base.SaveChangesAsync();
            throw new Exception("Need session data!");
        }

        /// <summary>
        /// Save changes with session data
        /// </summary>
        /// <param name="session">Session and user data</param>
        /// <returns>The number of state entries written to the underlying database.</returns>
        public virtual int SaveChanges(ISessionData session)
        {
            var addeds = ChangeTracker.Entries().Where(e => e.State == EntityState.Added);
            foreach (var entity in addeds)
            {
                SetInsertDefaults(entity, session);
            }

            var modifieds = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);
            foreach (var entity in modifieds)
            {
                SetUpdateDefaults(entity, session);
            }

            return base.SaveChanges();
        }

        public void SetInsertDefaults(DbEntityEntry entity, ISessionData session)
        {
            object o = entity.Entity;
            SetInsertDefaults(o, session);
        }

        public void SetInsertDefaults(object o, ISessionData session)
        {
            if (o is ICreateDate) ((ICreateDate)o).CreateDate = DateTime.Now;
            if (o is ICreatorIp) ((ICreatorIp)o).CreatorIp = session.GetUserIP();
            if (o is ICreatorId) ((ICreatorId)o).CreatorId = session.UserID;

            if (o is IModifyNr) ((IModifyNr)o).ModifyNr = 1;
            if (o is IModifyDate) ((IModifyDate)o).ModifyDate = DateTime.Now;
            if (o is IModifierIp) ((IModifierIp)o).ModifierIp = session.GetUserIP();
            if (o is IModifierId) ((IModifierId)o).ModifierId = session.UserID;
        }

        public void SetUpdateDefaults(DbEntityEntry entity, ISessionData session)
        {
            object o = entity.Entity;
            if (!saveWithStoredProcs && (o is ICreateDate || o is ICreatorIp || o is ICreatorId || o is IModifyNr))
            {
                var original = entity.GetDatabaseValues();
                if (o is ICreateDate) ((ICreateDate)o).CreateDate = original.GetValue<DateTime>("CreateDate");
                if (o is ICreatorIp) ((ICreatorIp)o).CreatorIp = original.GetValue<string>("CreatorIp");
                if (o is ICreatorId) ((ICreatorId)o).CreatorId = original.GetValue<string>("CreatorId");
                if (o is IModifyNr) ((IModifyNr)o).ModifyNr = original.GetValue<int>("ModifyNr") + 1;
            }

            if (o is IModifyDate) ((IModifyDate)o).ModifyDate = DateTime.Now;
            if (o is IModifierIp) ((IModifierIp)o).ModifierIp = session.GetUserIP();
            if (o is IModifierId) ((IModifierId)o).ModifierId = session.UserID;
        }

        /// <summary>
        /// Save changes with session data
        /// </summary>
        /// <param name="session">Session and user data</param>
        /// <returns>A task that represents the asynchronous save operation. The task result contains
        /// the number of state entries written to the underlying database.
        /// </returns>
        public async Task<int> SaveChangesAsync(ISessionData session)
        {
            var addeds = ChangeTracker.Entries().Where(e => e.State == EntityState.Added);
            foreach (var entity in addeds)
            {
                SetInsertDefaults(entity, session);
            }

            var modifieds = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);
            foreach (var entity in modifieds)
            {
                await SetUpdateDefaultsAsync(entity, session);
            }

            return await base.SaveChangesAsync();
        }

        public async Task SetUpdateDefaultsAsync(DbEntityEntry entity, ISessionData session)
        {
            object o = entity.Entity;
            if (!saveWithStoredProcs && (o is ICreateDate || o is ICreatorIp || o is ICreatorId || o is IModifyNr))
            {
                var original = await entity.GetDatabaseValuesAsync();
                if (o is ICreateDate) ((ICreateDate)o).CreateDate = original.GetValue<DateTime>("CreateDate");
                if (o is ICreatorIp) ((ICreatorIp)o).CreatorIp = original.GetValue<string>("CreatorIp");
                if (o is ICreatorId) ((ICreatorId)o).CreatorId = original.GetValue<string>("CreatorId");
                if (o is IModifyNr) ((IModifyNr)o).ModifyNr = original.GetValue<int>("ModifyNr") + 1;
            }

            if (o is IModifyDate) ((IModifyDate)o).ModifyDate = DateTime.Now;
            if (o is IModifierIp) ((IModifierIp)o).ModifierIp = session.GetUserIP();
            if (o is IModifierId) ((IModifierId)o).ModifierId = session.UserID;
        }
    }
}
