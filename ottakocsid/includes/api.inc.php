<?php

class API_INTERACTION {

    public static string $apiUrl = 'https://localhost:7130/api/cars';
    public static string $apiKey = 'szerveroldali-alkalmazasok-demo';
    public static function sendRequest($method, $url, $data = null)
    {
        $ch = curl_init();
        curl_setopt($ch, CURLOPT_URL, $url);
        curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
        curl_setopt($ch, CURLOPT_HTTPHEADER, [
            'X-Api-Key: ' . self::$apiKey,
            'Content-Type: application/json'
        ]);
        curl_setopt($ch, CURLOPT_SSL_VERIFYHOST, false);
        curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);
        curl_setopt($ch, CURLOPT_CUSTOMREQUEST, $method);

        if ($data) {
            curl_setopt($ch, CURLOPT_POSTFIELDS, json_encode($data));
        }

        $response = curl_exec($ch);
        if (curl_errno($ch)) {
            echo 'Hiba történt: ' . curl_error($ch);
            return null;
        }
        curl_close($ch);

        return json_decode($response, true);
    }
}