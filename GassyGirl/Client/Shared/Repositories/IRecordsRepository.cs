public interface IRecordsRepository
{
    List<MileageRecord> GetMileageRecords();
    List<Car> GetCars();
    bool SaveMileageRecord();
    bool SaveCar();
    bool DeleteMileageRecord();
    bool DeleteCar();
}
