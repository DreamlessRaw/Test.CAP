using System;
using Test.CAP.Utils;

namespace Test.CAP.Database
{
    public static class BaseEntityExtensions
    {
        public static void SaveOrUpdateOperation(this IBaseEntity entity)
        {
            var user = Current.User;
            if (entity.Id.Equals(0))
            {
                entity.CreateId = user.Id;
                entity.CreateName = $"{user.Code}_{user.Name}";
                entity.CreateTime = DateTime.Now;
            }

            entity.ModifyId = user.Id;
            entity.ModifyName = $"{user.Code}_{user.Name}";
            entity.ModifyTime = DateTime.Now;
        }

        public static void SaveOrUpdateOperation(this IBaseEntity entity, DateTime dateTime)
        {
            var user = Current.User;
            if (entity.Id.Equals(0))
            {
                entity.CreateId = user.Id;
                entity.CreateName = $"{user.Code}_{user.Name}";
                entity.CreateTime = dateTime;
            }

            entity.ModifyId = user.Id;
            entity.ModifyName = $"{user.Code}_{user.Name}";
            entity.ModifyTime = dateTime;
        }

        public static void SaveOrUpdateOperation(this IBaseEntity entity, LoginUser user)
        {
            if (entity.Id.Equals(0))
            {
                entity.CreateId = user.Id;
                entity.CreateName = $"{user.Code}_{user.Name}";
                entity.CreateTime = DateTime.Now;
            }

            entity.ModifyId = user.Id;
            entity.ModifyName = $"{user.Code}_{user.Name}";
            entity.ModifyTime = DateTime.Now;
        }

        public static void SaveOrUpdateOperation(this IBaseEntity entity, LoginUser user, DateTime dateTime)
        {
            if (entity.Id.Equals(0))
            {
                entity.CreateId = user.Id;
                entity.CreateName = $"{user.Code}_{user.Name}";
                entity.CreateTime = dateTime;
            }

            entity.ModifyId = user.Id;
            entity.ModifyName = $"{user.Code}_{user.Name}";
            entity.ModifyTime = dateTime;
        }
    }
}