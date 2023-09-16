using RealEstate.ViewModel;

namespace RealEstate.Services
{
    public interface IRealStateServices
    {
         Task Create(ViewFormModel model);
         Task Edit(ViewFormModel model);
         Task Delete(int? id);
    }
}
