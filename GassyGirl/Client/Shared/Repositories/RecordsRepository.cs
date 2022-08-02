internal class RecordsRepository : IRecordsRepository
{
    // Some private member variables we'll need
    private IDatabaseFacade _databaseFacade;
    private bool _dirty = true;
    private List<MileageRecord> _mileageRecords = new List<MileageRecord>();
    private List<Car> _cars = new List<Car>();

    public RecordsRepository(IDatabaseFacade databaseFacade)
    {
        _databaseFacade = databaseFacade ?? throw new ArgumentNullException(nameof(databaseFacade));
    }

    public List<MileageRecord> GetMileageRecords()
    {
        CacheData();

        return _mileageRecords;
    }

    public List<Car> GetCars()
    {
        CacheData();

        return _cars;
    }

    public bool SaveMileageRecord(MileageRecord mileageRecord)
    {
        _dirty = true;
        return _databaseFacade.SaveMileage(mileageRecord);
    }

    public bool SaveCar(Car car)
    {
        _dirty = true;
        return _databaseFacade.SaveCar(car);
    }

    public bool DeleteMileageRecord(MileageRecord mileageRecord)
    {
        _dirty = true;
        return _databaseFacade.DeleteMileage(mileageRecord);
    }

    public bool DeleteCar(Car car)
    {
        _dirty = true;
        return _databaseFacade.DeleteCar(car);
    }

    // This private method will fetch and process raw data if not already cached
    private void CacheData()
    {
        if (_dirty)
        {
            // Create the new car list and fetch the raw car data
            _cars = new List<Car>();
            var rawCarData = _databaseFacade.GetCars();

            // Loop over the raw data and populate the cars
            foreach (var record in rawCarData)
            {
                // Try to parse the id; if it's invalid, skip this record
                var validId = Guid.TryParse(record["id"].ToString(), out Guid id);
                if (!validId) continue;

                // Try to parse the year; if it's invalid, skip this record
                var validYear = int.TryParse(record["year"].ToString(), out int year);
                if (!validYear) continue;

                // Pick out the required fields; if any are null, skip this record
                var requiredFieldsValid = record["make"] != null && record["model"] != null && record["year"] != null;
                if (!requiredFieldsValid) continue;

                // Instantiate a new car and start populating its properties
                var car = new Car();
                car.Id = id;
                car.Make = record["make"]?.ToString() ?? "";
                car.Model = record["model"]?.ToString() ?? "";
                car.Trim = record["trim"]?.ToString() ?? "";
                car.Year = year;

                // Add the car to the collection
                _cars.Add(car);
            }

            // Create the new mileage list and fetch the raw mileage data
            _mileageRecords = new List<MileageRecord>();
            var rawMileageData = _databaseFacade.GetMileages();

            // Loop over the raw data and populate the mileage records
            foreach (var record in rawMileageData)
            {
                // Try to parse the id; if it's invalid, skip this record
                var validId = Guid.TryParse(record["id"].ToString(), out Guid id);
                if (!validId) continue;

                // Try to parse the date; if it's invalid, skip this record
                var validDate = DateTime.TryParse(record["date"].ToString(), out DateTime date);
                if (!validDate) continue;

                // Try to parse the trip odometer; if it's invalid, skip this record
                var validTripOdometer = Double.TryParse(record["trip_odometer"].ToString(), out Double tripOdometer);
                if (!validTripOdometer) continue;

                // Try to parse the trip odometer; if it's invalid, skip this record
                var validGallons = Double.TryParse(record["gallons"].ToString(), out Double gallons);
                if (!validGallons) continue;

                // Try to parse the trip odometer; if it's invalid, skip this record
                var validPricePerGallon = Double.TryParse(record["price_per_gallon"].ToString(), out Double pricePerGallon);
                if (!validPricePerGallon) continue;

                // Try to parse the trip odometer; if it's invalid, skip this record
                var validOdometer = int.TryParse(record["odometer"].ToString(), out int odometer);
                if (!validOdometer) continue;

                // Pick out the required fields; if any are null, skip this record
                var requiredFieldsValid = record["car_model"] != null;
                if (!requiredFieldsValid) continue;

                // Instantiate a new car and start populating its properties
                var mileageRecord = new MileageRecord();
                mileageRecord.Id = id;
                mileageRecord.Date = date;
                mileageRecord.CarModel = record["car_model"]?.ToString() ?? "";
                mileageRecord.TripOdometer = tripOdometer;
                mileageRecord.Gallons = gallons;
                mileageRecord.PricePerGallon = pricePerGallon;
                mileageRecord.Odometer = odometer;

                // Add the car to the collection
                _mileageRecords.Add(mileageRecord);
            }
        }
    }
}
