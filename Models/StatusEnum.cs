using ProjectManager.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.Models
{
    public enum StatusEnum
    {
        NEW,
        IN_PROGRESS, 
        FINISHED
    }

    [Table("Status")]
    public class Status : IEnumModel<Status, StatusEnum>
    {
        [Key]
        public StatusEnum Id { get; set; }
        public string Name { get; set; }
    }

    public static class EnumFunctions
    {
        public static IEnumerable<TModel> GetModelsFromEnum<TModel, TEnum>() where TModel : IEnumModel<TModel, TEnum>, new()
        {
            var enums = new List<TModel>();
            foreach (var enumVar in (TEnum[])Enum.GetValues(typeof(TEnum)))
            {
                enums.Add(new TModel
                {
                    Id = enumVar,
                    Name = enumVar.ToString()
                });
            }

            return enums;
        }
    }

    public interface IEnumModel<TModel, TModelIdType>
    {
        TModelIdType Id { get; set; }
        string Name { get; set; }
    }
}
