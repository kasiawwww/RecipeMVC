using Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.IRepositories
{
    public interface IRecipesRepo
    {
        RecipesDTO Add(RecipesDTO item);
        bool Update(RecipesDTO item);
        bool Delete(int id);
        RecipesDTO Get(int id);
        List<RecipesDTO> GetList();
    }
}
