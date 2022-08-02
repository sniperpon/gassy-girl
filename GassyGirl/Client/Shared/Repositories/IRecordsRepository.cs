public interface IRecordsRepository
{
    List<MileageRecord> GetMileageRecords();
    List<Car> GetCars();
    bool SaveMileageRecord(MileageRecord mileageRecord);
    bool SaveCar(Car car);
    bool DeleteMileageRecord(MileageRecord mileageRecord);
    bool DeleteCar(Car car);
}
