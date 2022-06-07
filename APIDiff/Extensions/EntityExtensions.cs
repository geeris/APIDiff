using APIDiff.DataAccess;
using APIDiff.DataAccess.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using static APIDiff.JsonUtility;

namespace APIDiff.Extensions
{

    public static class EntityExtensions
    {
        //Function used to get one object from an array of Left/Right entities
        public static Entity FindEntity(this IEnumerable<Entity> entities, int id)
        {
            return entities.Where(x => x.Id == id).FirstOrDefault();
        }

        //Function used to Add Left/Right entity to JSON
        public static void AddEntity(this DataDiff entities, Entity entity)
        {
            if (entity is Left left)
            {
                var leftArray = entities.Left.ToList();
                leftArray.Add(left);

                entities.Left = leftArray;
            }
            else if(entity is Right right)
            {
                var rightArray = entities.Right.ToList();
                rightArray.Add(right);

                entities.Right = rightArray;
            }

            var result = JsonSerializer.Serialize(entities, new JsonSerializerOptions { WriteIndented = true });
            WriteJsonIntoFile(result);
        }
    }
}
