<?php
class Menu
{
    public static array $menu = array();
    public static function setMenu(): void
    {
        self::$menu = array();

        // API URL és API kulcs
        $apiUrl = 'https://localhost:7130/api/pages';
        $apiKey = 'szerveroldali-alkalmazasok-demo';

        // cURL inicializálása
        $ch = curl_init();
        curl_setopt($ch, CURLOPT_URL, $apiUrl);
        curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
        curl_setopt($ch, CURLOPT_HTTPHEADER, [
            'X-Api-Key: ' . $apiKey,
            'Content-Type: application/json'
        ]);
        curl_setopt($ch, CURLOPT_SSL_VERIFYHOST, false);
        curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);

        // API kérés elküldése
        $response = curl_exec($ch);

        // Hibakezelés
        if (curl_errno($ch)) {
            echo 'Hiba történt az API kérés során: ' . curl_error($ch);
            curl_close($ch);
            return;
        }

        curl_close($ch);

        // JSON válasz dekódolása
        $menuItems = json_decode($response, true);

        // Ellenőrizzük, hogy az adatok helyesek-e
        if ($menuItems && is_array($menuItems)) {
            // Az adatok rendezése a 'sortingorder' mező alapján
            usort($menuItems, function ($a, $b) {
                return $a['sortingorder'] <=> $b['sortingorder'];
            });

            // Menü összeállítása a rendezett adatokból
            foreach ($menuItems as $menuitem) {
                // Jogosultsági ellenőrzés
                if (
                    ($menuitem['permission'][0] === '1' && $_SESSION['userlevel'][0] === '1') ||
                    ($menuitem['permission'][1] === '1' && $_SESSION['userlevel'][1] === '1') ||
                    ($menuitem['permission'][2] === '1' && $_SESSION['userlevel'][2] === '1')
                ) {
                    self::$menu[$menuitem['url']] = [
                        $menuitem['page1'],
                        $menuitem['permission']
                    ];
                }
            }
        }
    }


    public static function getMenu($sItems): string
    {
        $menu = "<div class=\"navbar\">" .
            "<div class=\"logo\"><a href=\"/ottakocsid/home\"><img src=\"/ottakocsid/" . IMG . "logo.png\" alt=\"Logó\"></a></div>";
        $menu .= "<div class=\"menu\">";
        foreach (self::$menu as $menuindex => $menuitem) {
            $menu .= "<a href='/ottakocsid/" . $menuindex . "' " .
                ($menuindex == $sItems[0] ? "class='activenav'" : "") . ">" .
                $menuitem[0] . "</a>";
        }
        $menu .= "</div></div>";
        return $menu;
    }
}
Menu::setMenu();
?>
