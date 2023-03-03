using zamara.Models;
using Zamara.Models;

namespace zamara.IService;

public interface IStaffService {
    public Task<Staff> CreateStaff(StaffDto staff);
    public List<Staff> GetAllStaff();
    public Task<Staff> GetStaff(string id);
    public Task<Staff> EditStaffAsync(StaffDto staff);
    public Task<bool> DeleteStaffAsync(string id);

}