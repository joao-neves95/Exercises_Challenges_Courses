<?php 
$data = $_POST["data"];

$file = fopen("../json/importances.json", 'w') or die("can't open file");
fwrite($file, $data);
fclose($file);

echo json_encode(array("success"=> true));

?>