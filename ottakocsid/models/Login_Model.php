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

    public function changePassword($username, $newPassword): void
    {
        $existingUserData = $this->fetchUserData($username);
        if (empty($existingUserData)) {
            throw new Exception("Felhasználó nem található: $username");
        }
        $existingUserData['hashedPsw'] = password_hash($newPassword, PASSWORD_DEFAULT);
        $url = "https://localhost:7130/api/users/$username";
        API_INTERACTION::sendRequest('PUT', $url, $existingUserData);
    }

}