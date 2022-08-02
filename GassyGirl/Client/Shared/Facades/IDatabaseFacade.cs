public interface IDatabaseFacade
{
    List<Dictionary<string, object>> GetMileages();
    List<Dictionary<string, object>> GetCars();
    bool SaveMileage(MileageRecord mileageRecord);
    bool SaveCar(Car car);
    bool DeleteMileage(MileageRecord mileageRecord);
    bool DeleteCar(Car car);
}
