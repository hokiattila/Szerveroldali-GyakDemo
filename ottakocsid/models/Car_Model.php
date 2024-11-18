<?php
namespace models;

include(SERVER_ROOT.'includes/api.inc.php');

use DateTime;
use API_INTERACTION;
class Car_Model
{

    public function fetchCarData($offset = false, $limit = false): array
    {
        $url = API_INTERACTION::$apiUrl . "?offset=$offset&limit=$limit";
        return API_INTERACTION::sendRequest('GET', $url) ?? [];
    }

    public function getCarRowCount(): int
    {
        $cars = $this->fetchCarData();
        return count($cars);
    }

    public function fetchBrandNames(): array|bool
    {
        // Lekérjük az összes autó adatot
        $cars = $this->fetchCarData(0,10000000);
        // Ellenőrizzük, hogy a válasz tömb-e
        if (!is_array($cars) || empty($cars)) {
            echo "Hiba történt a fetchBrandNames metódusban: Az autó adatok érvénytelenek.";
            return false;
        }
        // Kiszűrjük az egyedi márkaneveket
        $brands = [];
        foreach ($cars as $car) {
            // Ellenőrizzük, hogy a 'brand' kulcs létezik-e
            if (isset($car['brand']) && !in_array($car['brand'], $brands)) {
                $brands[] = $car['brand'];
            }
        }
        return $brands;
    }
    public function checkVIN(string $vin): bool
    {
        $cars = $this->fetchCarData(0,10000000);
        if (!is_array($cars) || empty($cars)) {
            echo "Hiba történt a checkVIN metódusban: Az autó adatok érvénytelenek.";
            return false;
        }
        foreach ($cars as $car) {
            if (isset($car['vin']) && $car['vin'] === $vin) {
                return true;
            }
        }
        return false;
    }


    public function fetchIndividualCar($vin): array|bool
    {
        $url = API_INTERACTION::$apiUrl . "/$vin";
        return API_INTERACTION::sendRequest('GET', $url) ?? false;
    }

    public function fetchImages($VIN)
    {
        $directory = $_SERVER["DOCUMENT_ROOT"] . "/ottakocsid/public/img/cars/" . $VIN;
        $allowed_types = ['jpg', 'jpeg', 'png', 'gif', 'webp'];
        $files = [];
        $dir_handle = @opendir($directory) or die("Hiba történt a könyvtár megnyitásakor!");

        while ($file = readdir($dir_handle)) {
            $extension = strtolower(pathinfo($file, PATHINFO_EXTENSION));
            if (in_array($extension, $allowed_types)) {
                $files[] = $file;
            }
        }
        closedir($dir_handle);
        return $files;
    }

    public function insertCar($vin, $brand, $modell, $build_year, $door_count, $color, $weight, $power, $con, $fuel_type, $price): void
    {
        $data = [
            'vin' => $vin,
            'brand' => $brand,
            'modell' => $modell,
            'buildYear' => (int)(new DateTime($build_year))->format('Y'),
            'doorCount' => $door_count,
            'color' => $color,
            'weight' => $weight,
            'power' => $power,
            'con' => $con,
            'fuelType' => $fuel_type,
            'price' => $price,
        ];
        API_INTERACTION::sendRequest('POST', API_INTERACTION::$apiUrl, $data);
    }

    public function deleteCar($vin): void
    {
        $url = API_INTERACTION::$apiUrl . "/$vin";
        API_INTERACTION::sendRequest('DELETE', $url);
    }
}
