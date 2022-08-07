let databaseName = "GassyGirl";
let databaseVersion = 1;
let carStore = "car";
let mileageStore = "mileage";
let maintenanceStore = "maintenance";
let settingsStore = "settings";
let primaryKey = "id";


//=========================//
// Database Event Handlers //
//=========================//

// Open the database
let openRequest = indexedDB.open(databaseName, databaseVersion);

// This event handler performs database updates if needed
openRequest.onupgradeneeded = function(event) {

    // Old version is the existing database version for this user
    switch(event.oldVersion) {
        case 0:
            // Client has no database, so perform initialization
            initializeDatabase();
        case 1:
            // Put upgrade logic in here if we make a version 2
    }
};

// This event handler picks up errors
openRequest.onerror = function() {
    console.error("Error", openRequest.error);
};

// This event handler is where work proceeds
openRequest.onsuccess = function() {
    let database = openRequest.result;

    // The version changed while they were using the application!
    database.onversionchange = function() {
        database.close();
        alert("Database is outdated, please reload the page.")
    };


};


//================//
// CRUD Functions //
//================//

window.getCars = () => {
    return fetchCars().then((data) => { return data});
}

function fetchCars() {
    return new Promise((resolve, reject) => {
        let database = openRequest.result;
    
        // Open up a transaction and fetch the object store
        let transaction = database.transaction(carStore, "readonly");
        let carsData = transaction.objectStore(carStore);
    
        // Fetch all of the cars
        let allCarsData = carsData.getAll();
    
        // This handles successful data retrieval
        allCarsData.onsuccess = function() {
            resolve(allCarsData.result);
        };

        // This handles an error in data retrieval
        allCarsData.onerror = function() {
            reject();
        };
    });
}

window.getMileages = () => {
    return fetchMileages().then((data) => { return data});
}

function fetchMileages() {
    return new Promise((resolve, reject) => {
        let database = openRequest.result;
    
        // Open up a transaction and fetch the object store
        let transaction = database.transaction(mileageStore, "readonly");
        let mileageData = transaction.objectStore(mileageStore);
    
        // Fetch all of the mileages
        let allMileageData = mileageData.getAll();
    
        // This handles successful data retrieval
        allMileageData.onsuccess = function() {
            resolve(allMileageData.result);
        };

        // This handles an error in data retrieval
        allMileageData.onerror = function() {
            reject();
        };
    });
}

window.getMaintenances = () => {
    return fetchMaintenances().then((data) => { return data});
}

function fetchMaintenances() {
    return new Promise((resolve, reject) => {
        let database = openRequest.result;
    
        // Open up a transaction and fetch the object store
        let transaction = database.transaction(maintenanceStore, "readonly");
        let maintenanceData = transaction.objectStore(maintenanceStore);
    
        // Fetch all of the cars
        let allMaintenanceData = maintenanceData.getAll();
    
        // This handles successful data retrieval
        allMaintenanceData.onsuccess = function() {
            resolve(allMaintenanceData.result);
        };

        // This handles an error in data retrieval
        allMaintenanceData.onerror = function() {
            reject();
        };
    });
}

window.saveCar = (car) => {
    let database = openRequest.result;

    // Open up a transaction and fetch the object store
    let transaction = database.transaction(carStore, "readwrite");
    let carsData = transaction.objectStore(carStore);

    // Perform the put, which intelligently inserts or updates
    let putRequest = carsData.put(car);

    // If the put worked, log a message
    putRequest.onsuccess = function() {
        console.log("Car added to the store", putRequest.result);
    }

    // If the put failed, log the error
    putRequest.onerror = function() {
        console.log("Error while adding the car", putRequest.error);
    }
}

window.saveMileage = (mileage) => {
    let database = openRequest.result;

    // Open up a transaction and fetch the object store
    let transaction = database.transaction(mileageStore, "readwrite");
    let mileageData = transaction.objectStore(mileageStore);

    // Perform the put, which intelligently inserts or updates
    let putRequest = mileageData.put(mileage);

    // If the put worked, log a message
    putRequest.onsuccess = function() {
        console.log("Mileage added to the store", putRequest.result);
    }

    // If the put failed, log the error
    putRequest.onerror = function() {
        console.log("Error while adding the mileage", putRequest.error);
    }
}

window.saveMaintenance = (maintenance) => {
    let database = openRequest.result;

    // Open up a transaction and fetch the object store
    let transaction = database.transaction(maintenanceStore, "readwrite");
    let maintenanceData = transaction.objectStore(maintenanceStore);

    // Perform the put, which intelligently inserts or updates
    let putRequest = maintenanceData.put(maintenance);

    // If the put worked, log a message
    putRequest.onsuccess = function() {
        console.log("Maintenance added to the store", putRequest.result);
    }

    // If the put failed, log the error
    putRequest.onerror = function() {
        console.log("Error while adding the maintenance", putRequest.error);
    }
}

window.deleteCar = (car) => {
    let database = openRequest.result;

    // Open up a transaction and fetch the object store
    let transaction = database.transaction(carStore, "readwrite");
    let carsData = transaction.objectStore(carStore);

    // Delete the car
    let deleteRequest = carsData.delete(car.id);

    // If the delete worked, then perform the add
    deleteRequest.onsuccess = function() {
        console.log("Car deleted from store", deleteRequest.result);
    };

    // If the delete request failed, log the error
    deleteRequest.onerror = function() {
        console.log("Error while deleting the car", deleteRequest.error);
    };
}

window.deleteMileage = (mileage) => {
    let database = openRequest.result;

    // Open up a transaction and fetch the object store
    let transaction = database.transaction(mileageStore, "readwrite");
    let mileageData = transaction.objectStore(mileageStore);

    // Delete the mileage
    let deleteRequest = mileageData.delete(mileage.id);

    // If the delete worked, then perform the add
    deleteRequest.onsuccess = function() {
        console.log("Mileage deleted from store", deleteRequest.result);
    };

    // If the delete request failed, log the error
    deleteRequest.onerror = function() {
        console.log("Error while deleting the mileage", deleteRequest.error);
    };
}

window.deleteMaintenance = (maintenance) => {
    let database = openRequest.result;

    // Open up a transaction and fetch the object store
    let transaction = database.transaction(maintenanceStore, "readwrite");
    let maintenanceData = transaction.objectStore(maintenanceStore);

    // Delete the mileage
    let deleteRequest = maintenanceData.delete(maintenance.id);

    // If the delete worked, then perform the add
    deleteRequest.onsuccess = function() {
        console.log("Maintenance deleted from store", deleteRequest.result);
    };

    // If the delete request failed, log the error
    deleteRequest.onerror = function() {
        console.log("Error while deleting the maintenance", deleteRequest.error);
    };
}


//==================//
// Helper Functions //
//==================//

// This function will initialize the database
function initializeDatabase() {
    let database = openRequest.result;

    // Create the car object store if we need to
    if (!database.objectStoreNames.contains(carStore)) {
        database.createObjectStore(carStore, {keyPath: primaryKey});
    }

    // Create the mileage object store if we need to
    if (!database.objectStoreNames.contains(mileageStore)) {
        database.createObjectStore(mileageStore, {keyPath: primaryKey});
    }

    // Create the maintenance object store if we need to
    if (!database.objectStoreNames.contains(maintenanceStore)) {
        database.createObjectStore(maintenanceStore, {keyPath: primaryKey});
    }

    // Create the settings object store if we need to
    if (!database.objectStoreNames.contains(settingsStore)) {
         database.createObjectStore(settingsStore, {keyPath: primaryKey});
    }
}

// This function lets us delete the database if need be
function deleteDatabase() {
    let deleteRequest = indexedDB.deleteDatabase(databaseName)
}
