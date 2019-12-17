using Microsoft.AspNetCore.Mvc.Rendering;
using RepairServiceCenterASP.Models;
using System.Collections.Generic;

namespace RepairServiceCenterASP.ViewModels.Filters
{
    public class TypesOfFaultsFilter
    {
        public int? SelectedModel { get; private set; }
        public SelectList Models { get; private set; }
        public string InputName { get; private set; }
        public string InputMethodRepair { get; private set; }
        public string InputClient { get; private set; }

        public TypesOfFaultsFilter(List<RepairedModel> models, int? model, string name,
            string methodRepair, string client)
        {
            models.Insert(0, new RepairedModel(){ RepairedModelId = 0, Name = "Все" });
            Models = new SelectList(models, "RepairedModelId", "Name", model);

            InputName = name;
            InputMethodRepair = methodRepair;
            InputClient = client;
        }
    }
}
