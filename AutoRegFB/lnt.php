<?php
$type = $_GET["t"];
$username = $_GET["e"];
$pass = 'admin123';
$codes = array( '323',
                '408',
                '415',
                '424',
                '510',
                '530',
                '559',
                '562',
                '619',
                '626',
                '650',
                '657',
                '661',
                '707',
                '714',
                '760',
                '805',
                '818',
                '831',
                '858',
                '925',
                '949',
                '951');
$err = "Error...!!!";
$done = "All done...!!!";
switch ($type) {
    case "1":
        //GetMobile
        $login = Login($username, $pass);
        if($login[1] != null){
            $tptn = '+1'.$login[1]["voiceConf"]["tptn"];
            echo $tptn;
        }else {
            LogOut();
            echo $err;
        }
        break;
    case "2":
        //GetMessage
        Login($username, $pass);
        $msg = GetMessages();
        echo @json_encode($msg);
        //LogOut();
        break;
    case "3":
        //ChangeNumber
        $code = $codes[array_rand($codes)];
        Login($username, $pass);
        $change = ChangeNumber($code);
        //$out = array_values($change);
        echo @json_encode($change);
        LogOut();
        break;
    case "4":
        //Reg
        $name = 'Thuy Nho';
        $birthday = '12/30/1985';
        $reg = Register($username, $name, $pass, $birthday);
        $code = $codes[array_rand($codes)];
        Login($username, $pass);
        $change = ChangeNumber($code);
        //$out = array_values($change);
        echo @json_encode($change);
        LogOut();
        break;
	case "5":
		ini_set('max_execution_time', 300); //300 seconds = 5 minutes
		//hideChatGroup
		try {
			Login($username, $pass);
			$msgs = GetMessages();
			foreach ($msgs['conversations']['conversation'] as $msg) {
				$cid = $msg['@attributes']['cid'];
				HideChatGroup($cid);			
			}
			echo $done;
			//LogOut();
		} catch (Exception $e) {
			echo $err;
		}
		break;
    default:
        echo $err;
        break;
}

function SendRequest($url, $sub_url, $post, $data) {
    $basics = array('app' => 'textplusfree.599',
                    'dev' => 'android.t03g',
                    'udid' => '6a672a4c-2119-48b6-b992-73cbdc6cc167',
                    'language_device' => 'en', 
                    'osversion' => '4.1.2',
                    'timezone' => 'GMT+07:00',
                    'market' => 'default',
                    'token' => '158e3d1bdedae3f10d741a772900a7cf',
                    'country_device' => 'US');
                    
    $q_data = array_merge($basics, $data);    
    $data = http_build_query($q_data);
     
    // Define the main URL that will handle requests
    $base = 'https://'.$sub_url.'.gogii.com/';
    
    if($post) {
        $final_url = $base.$url;
    } else {
        $final_url = $base.$url.'?'.$data;
    }
    //echo $final_url;   

    $ch = curl_init();
    curl_setopt($ch, CURLOPT_URL, $final_url);
    curl_setopt($ch, CURLOPT_USERAGENT, 'Dalvik/1.6.0 (Linux; U; Android 4.1.2; GT-N7100 Build/JZO54K)');
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);
    curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);

    if($post) { 
        curl_setopt($ch, CURLOPT_POST, true);
        curl_setopt($ch, CURLOPT_POSTFIELDS, $q_data);
    }
        
    $response = curl_exec($ch);

    $http = curl_getinfo($ch, CURLINFO_HTTP_CODE);
    curl_close($ch);
        
    return array($http, $response);
}   
                
function Register($username, $name, $pass, $birthday) {
    // Birthday must be of the following format
    // month/day/year
    // 6/24/2000
    $data = array('alias' => $username,
                'aliasType' => 'EMAIL',
                'sendValidation' => 0);
    $info = SendRequest('api/createAlias.json', 'android', false, $data);
    $part_one = @json_decode($info[1], true);
    
    if($info[0] == 504) {
        return "Invalid Token"; 
    } else {
        if($part_one['result'] == "Alias used by other member") {
            return "User already exists with that email";
        } else {
            $data = array(
                        'facebooknotification' => 0,
                        'birthdate' => $birthday,
                        'notValidatedAlias' => $username,
                        'password' => $pass,
                        'nickname' => $name,
                        'countrycode' => 'US',
                        'gender' => 'Male',
                        'noVoice' => 0,
                        'nativeAddressBookAutoSync' => 1,
                        'noTptn' => 1,
                        'notValidatedAliasType' => 'EMAIL');
    
            $info = SendRequest('api/voice/fullRegistration.json', 'android', false, $data);
            $part_two = @json_decode($info[1], true);
        
            return array($part_one, $part_two);
        }
    }
}   
function HideChatGroup($cid)
{
	$data = array('cid' => $cid);
	$info = SendRequest('api/hideChatGroup', 'android', false, $data);
	$xml = simplexml_load_string($info[1]);
	$json = json_encode($xml);
	$array = json_decode($json, true);
	return $array;
}
function Login($user, $pass) {
    $data = array('password' => $pass,
                'username' => $user,
                'aliasType' => 'EMAIL',                
                'voiceApp' => 1);
    // Log the user in
    $info = SendRequest('api/login.json', 'android', false, $data);
    $login = @json_decode($info[1], true);      
    if($info[0] == 504) {
        return "Invalid Token";
    } else {
        // Get the startup info
        $start = StartUpInfo();
        $startup = @json_decode($start[1], true);        
        // Get the member ID
        $login_id = $login['memberId'];
		// Do the oAuth         
        $data = array('apns' => 'aec7566c88138d8338e14606fee1f6dbf958051b21616f21e8da30488ad643d',
                    'checkVersion' => 1,
                    'client_id' => 'my-trusted-client',
                    'grant_type' => 'exchange_token',
                    'memberId' => $login_id,
                    'scope' => 'trust',
                    'userInfo' => 1,
                    'pmsg' => 0);
        
        $info = SendRequest('oauth/token', 'vb', false, $data);     
        $decode = @json_decode($info[1], true);		
        return array($login, $startup, $decode);
    } 
}

function StartUpInfo() {
    $data = array('dw' => 720,
                'alias' => 1,
                'hconf' => 1,
                'voiceConf' => 1,
                'dh' => 1280,
                'userInfo' => 1,
                'country_device' => 'US',
                'startSession' => 1,
                'allProductAccess' => 1);
    $info = SendRequest('api/startupInfo.json', 'android', false, $data);
    return $info;
}

function GetMessages() {
    $data = array();
    $info = SendRequest('api/getConvosByTime', 'android', false, $data); 
        
    if($info[0] == 504) {
            return "Invalid Token";
    } else {
        // Parse the XML
        $xml = simplexml_load_string($info[1]);
        $json = json_encode($xml);
        $array = json_decode($json, true);
        
        return $array;
    }
}

function SendMessage($to, $msg) {
    $data = array('cMsgId' => '+1'.$to.'-pend_430826497.742299-1',
                'message' => $msg,
                'recipient0' => '+1'.$to);
    $info = SendRequest('api/chat/postOrCreate', 'android', true, $data);
        
    if($info[0] == 504) {
        return "Invalid Token";
    } else {
        // Parse the XML
        $xml = simplexml_load_string($info[1]);
        $json = json_encode($xml);
        $array = json_decode($json, true);
        
        return $array;
    }
}

function ChangeNumber($area_code) {
    $data = array('areaCode' => $area_code,
                'setVoiceTptn' => 1);
    $info = SendRequest('api/tptn/OwnTpTnByAreaCode.json', 'tptn', false, $data);
    $decode = @json_decode($info[1], true);
    return $decode;
}
    
function LogOut() {
    $data = array();
    $info = SendRequest('api/logout', 'android', false, $data); 
        
    if($info[0] == 504) {
        return "Invalid Token";
    } else {
        // Parse the XML
        $xml = simplexml_load_string($info[1]);
        $json = json_encode($xml);
        $array = json_decode($json, true);
        
        return $array;
    }
}


?>
