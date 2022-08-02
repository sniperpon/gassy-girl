using Microsoft.Data.Sqlite;

internal class DatabaseFacade : IDatabaseFacade
{
    // Here are some constants we need
    private const string DatabasePath = "GassyGirl.db";
    private const string ConnectionString = "Data Source=file:" + DatabasePath + ";Mode=ReadWriteCreate";

    // Here are some required member variables
    private SqliteConnection connection = new SqliteConnection(ConnectionString);

    // This public method will fetch all cars
    public List<Dictionary<string, object>> GetCars()
    {
        PrepareDatabase();

        // Prepare for the data and define the query
        var data = new List<Dictionary<string, object>>();
        var query = "select * from car;";

        // Execute the query
        var command = connection.CreateCommand();
        command.CommandText = query;
        
        // Loop over the data and build up the records
        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                var recordData = new Dictionary<string, object>();

                // Populate the key value pairs
                recordData.Add("id", reader.GetString(0));
                recordData.Add("make", reader.GetString(1));
                recordData.Add("model", reader.GetString(2));
                recordData.Add("trim", reader.GetString(3));
                recordData.Add("year", reader.GetString(4));

                // Add the record data to the parent list
                data.Add(recordData);
            }
        }

        // Return the data
        return data;
    }

    // This public method will fetch all mileages
    public List<Dictionary<string, object>> GetMileages()
    {
        PrepareDatabase();

        // Prepare for the data and define the query
        var data = new List<Dictionary<string, object>>();
        var query = "select * from mileage order by date desc;";

        // Execute the query
        var command = connection.CreateCommand();
        command.CommandText = query;

        // Loop over the data and build up the records
        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                var recordData = new Dictionary<string, object>();

                // Populate the key value pairs
                recordData.Add("id", reader.GetString(0));
                recordData.Add("date", reader.GetString(1));
                recordData.Add("car_model", reader.GetString(2));
                recordData.Add("trip_odometer", reader.GetDecimal(3));
                recordData.Add("gallons", reader.GetDecimal(4));
                recordData.Add("price_per_gallon", reader.GetDecimal(5));
                recordData.Add("odometer", reader.GetInt32(6));

                // Add the record data to the parent list
                data.Add(recordData);
            }
        }

        // Return the data
        return data;
    }

    // This public method will save a mileage
    public bool SaveMileage(MileageRecord mileageRecord)
    {
        PrepareDatabase();
        var saveWorked = true;

        // Define the statements we'll need
        var deleteStatement = string.Format("delete from mileage where Id = '{0}';", mileageRecord.Id.ToString());
        var insertStatement = string.Format("insert into mileage values ('{0}', '{1}', '{2}', {3}, {4}, {5}, {6});", mileageRecord.Id.ToString(), mileageRecord.Date.ToString(), mileageRecord.CarModel, mileageRecord.TripOdometer, mileageRecord.Gallons, mileageRecord.PricePerGallon, mileageRecord.Odometer);

        // Execute the statements
        using (var transaction = connection.BeginTransaction())
        {
            // Perform the delete first
            var deleteCommand = connection.CreateCommand();
            deleteCommand.CommandText = deleteStatement;
            deleteCommand.ExecuteNonQuery();

            // Perform the insert next
            var insertCommand = connection.CreateCommand();
            insertCommand.CommandText = insertStatement;
            var recordsInserted = insertCommand.ExecuteNonQuery();

            // Either roll back or commit the transaction
            if (recordsInserted == 0)
            {
                transaction.Rollback();
                saveWorked = false;
            }
            else
                transaction.Commit();
        }

        // Return whether things worked
        return saveWorked;
    }

    // This public method will save a mileage
    public bool SaveCar(Car car)
    {
        PrepareDatabase();
        var saveWorked = true;

        // Define the statements we'll need
        var deleteStatement = string.Format("delete from car where Id = '{0}';", car.Id);
        var insertStatement = string.Format("insert into car values ('{0}', '{1}', '{2}', '{3}', {4});", car.Id, car.Make, car.Model, car.Trim, car.Year);

        // Execute the statements
        using (var transaction = connection.BeginTransaction())
        {
            // Perform the delete first
            var deleteCommand = connection.CreateCommand();
            deleteCommand.CommandText = deleteStatement;
            deleteCommand.ExecuteNonQuery();

            // Perform the insert next
            var insertCommand = connection.CreateCommand();
            insertCommand.CommandText = insertStatement;
            var recordsInserted = insertCommand.ExecuteNonQuery();

            // Either roll back or commit the transaction
            if (recordsInserted == 0)
            {
                transaction.Rollback();
                saveWorked = false;
            }
            else
                transaction.Commit();
        }

        // Return whether things worked
        return saveWorked;
    }

    // This public method will save a mileage
    public bool DeleteMileage(MileageRecord mileageRecord)
    {
        PrepareDatabase();

        // Define the statements we'll need
        var deleteStatement = string.Format("delete from mileage where Id = {0};", mileageRecord.Id);

        // Execute the command
        var deleteCommand = connection.CreateCommand();
        deleteCommand.CommandText = deleteStatement;
        deleteCommand.ExecuteNonQuery();

        // Let them know it worked
        return true;
    }

   // This public method will save a mileage
    public bool DeleteCar(Car car)
    {
        PrepareDatabase();

        // Define the statements we'll need
        var deleteStatement = string.Format("delete from car where Id = {0};", car.Id);

        // Execute the command
        var deleteCommand = connection.CreateCommand();
        deleteCommand.CommandText = deleteStatement;
        deleteCommand.ExecuteNonQuery();

        // Let them know it worked
        return true;
    }

    // This private method will construct the database if it does not exist
    private void PrepareDatabase()
    {
        // Create the actual database file if it's missing
        if (!File.Exists(DatabasePath)) File.Create(DatabasePath);

        // Open the connection if it is closed
        if (connection.State == System.Data.ConnectionState.Closed)
            connection.Open();

        System.Console.WriteLine("here1: " + Directory.GetFiles("/")[0]);

        // See if the car table exists already
        var carTableQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='car';";
        SqliteCommand carTableCommand = new SqliteCommand(carTableQuery, connection);

        // Create the table if it does not exist
        using (SqliteDataReader carTableReader = carTableCommand.ExecuteReader())
        {
            if(!carTableReader.HasRows)
            {
                System.Console.WriteLine("here2");
                // Define the car table
                var carCreateStatement = @"CREATE TABLE car(
                    id TEXT PRIMARY KEY,
                    make TEXT,
                    model TEXT,
                    trim TEXT,
                    year INTEGER
                )";

                // Create the car table
                using var carCreateCommand = new SqliteCommand(carCreateStatement, connection);
                carCreateCommand.ExecuteNonQuery();
            }
        }

        // See if the mileage table exists already
        var mileageTableQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='mileage';";
        SqliteCommand mileageTableCommand = new SqliteCommand(mileageTableQuery, connection);

        // Create the table if it does not exist
        using (SqliteDataReader mileageTableReader = mileageTableCommand.ExecuteReader())
        {
            if(!mileageTableReader.HasRows)
            {
                // Define the mileage table
                var mileageCommandText = @"CREATE TABLE mileage(
                    id TEXT PRIMARY KEY,
                    date TEXT,
                    car_model TEXT,
                    trip_odometer DECIMAL(10, 5),
                    gallons DECIMAL(10, 5),
                    price_per_gallon DECIMAL(10, 5),
                    odometer INTEGER
                )";

                // Create the mileage table
                using var mileageCommand = new SqliteCommand(mileageCommandText, connection);
                mileageCommand.ExecuteNonQuery();
            }
        }
    }
}
