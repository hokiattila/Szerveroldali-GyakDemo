<?php
use models\Car_Model;
require_once(SERVER_ROOT . 'models/view_loader.php');
include(SERVER_ROOT.'models/Car_Model.php');
class Car_Controller
{
    public string $baseName;
    public Car_Model $model;
    public array|bool $car;
    public array|bool $carIMG;
    public string|false $VIN;


    public function __construct()
    {
        $this->baseName = "car";
        $this->model = new Car_Model();
        $this->car = false;
        $this->carIMG = false;
        $this->VIN = false;
    }

    public function searchVIN(array $array, string $VIN): bool {
        foreach ($array as $item)
            if (isset($item["car_VIN"]) && $item["car_VIN"] === $VIN)  return true;
        return false;
    }

    public function main(array $vars): void {
        if ($_SERVER['REQUEST_METHOD'] == "POST") {
            if ($vars[0] != "delete" || empty($vars[1]) || !($this->model->checkVIN($vars[1]))) {
                $view = new View_Loader("error_main");
                return;
            }
                $this->model->deleteCar($vars[1]);
                header("Location: /ottakocsid/home");
            } else {
                $this->car = $this->model->fetchIndividualCar($vars[0]);
                $this->carIMG = $this->model->fetchImages($vars[0]);
                $this->VIN = $vars[0];

                $view = new View_Loader($this->baseName . "_main");
                $view->assign("car", $this->car);
                $view->assign("carIMG", $this->carIMG);
                $view->assign("VIN", $this->VIN);
            }
        }
}
