<?php
include(SERVER_ROOT.'models/Login_Model.php');
include(SERVER_ROOT.'includes/token.inc.php');
include(SERVER_ROOT.'includes/error.inc.php');

use models\Login_Model;

class Admin_Controller {
    public string $baseName;
    public Login_Model $model;
    public function __construct() {
        $this->model = new Login_Model();
        $this->baseName = "admin";
    }

    public function main(array $vars) {
        if($_SERVER["REQUEST_METHOD"] == "GET") {
            if($_SESSION['userlevel'] != '__1' && $_SESSION['username'] == "unknown") {
                $view = new View_Loader("login_main");
            } else {
                $view = new View_Loader('advert_main');
            }
            $view->assign('token', Token::generateToken());
        } else if($_SERVER['REQUEST_METHOD'] == "POST" && key_exists("login-btn",$vars)) {
            $token = $vars['token'];
            $username_input = $vars['username'];
            $password_input = $vars['password'];

            if($token != $_SESSION['token']) Raise_Error::raiseError($this,"Sikertelen token validálás");
            else if(empty($username_input) || empty($password_input)) Raise_Error::raiseError($this,"Hiányzó felhasználónév vagy jelszó");
            else {
                $res = $this->model->fetchUserData("admin");
                if(!empty($res) && $res['username'] == $username_input && password_verify($password_input, $res['hashedPsw'])) {
                    $_SESSION['username'] = $res['username'];
                    $_SESSION['userfirstname'] = $res['firstName'];
                    $_SESSION['userlastname'] = $res['lastName'];
                    $_SESSION['userlevel'] = $res['permission'];
                    $_SESSION['login-try'] = "success";
                    header("Location: /ottakocsid/home");
                    exit;
                } else {
                    Raise_Error::raiseError($this,"Hibás felhasználónév vagy jelszó");
                }
            }
        } else if($_SERVER['REQUEST_METHOD'] == "POST" && key_exists("change-btn",$vars)) {
                $current = $vars['current_psw'];
                $new = $vars['new_psw'];
                $new_conf = $vars['new_psw_conf'];
                $res = $this->model->fetchUserData("admin");
                if(!empty($res)  && password_verify($current, $res['hashedPsw']) && $new == $new_conf) {
                    $this->model->changePassword("admin", $new);
                    header("Location: /ottakocsid/logout");
                } else {
                    $_SESSION['error'] = "Jelszóhiba!";
                    $view = new View_Loader('advert_main');                }
        }
    }

}