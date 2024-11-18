<?php
namespace models;
include(SERVER_ROOT.'includes/api.inc.php');

use API_INTERACTION;

class Login_Model {
    public function fetchUserData($username): array {
        $url = "https://localhost:7130/api/users/$username";
        $response = API_INTERACTION::sendRequest('GET', $url);
        if ($response && is_array($response)) {
            return $response;
        }
        return [];
    }


}