using AutoUpdaterDotNET;
using Gecko;
using Gecko.DOM;
using MyUtility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RandomNameGenerator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoRegFB
{
    /// <summary>
    /// AutoRegFB
    /// Author: nhothuy48cb@gmail.com
    /// FB: https://facebook.com/nhothuy
    /// </summary>
    public partial class frmMain : Form
    {
        #region "PARAMS"
        int STEP = 0;
        System.Windows.Forms.Timer TIMER_REG = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer TIMER_PLAY = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer TIMER_ACCEPT = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer TIMER_INVITE = new System.Windows.Forms.Timer();
        private String FILENAME_FBIDS = String.Format("{0}\\acc.txt", Path.GetDirectoryName(Application.ExecutablePath));
        private String FILENAME_FBS = String.Format("{0}\\acc-invite.txt", Path.GetDirectoryName(Application.ExecutablePath));
        private String FILENAME_PROXY = String.Format("{0}\\proxy.txt", Path.GetDirectoryName(Application.ExecutablePath));
        private String FILENAME_FBIDS_OUT = String.Format("{0}\\fbids.txt", Path.GetDirectoryName(Application.ExecutablePath));
        private String FILENAME_FILE_HTML_SAVE = String.Format("{0}\\tmp.html", Path.GetDirectoryName(Application.ExecutablePath));
        private List<Acc> ACCOUNTS = new List<Acc>();
        private List<Acc> ACCOUNTSFB = new List<Acc>();
        private String URL = "http://222.255.29.210:9000/lnt.php?t={0}&e={1}";
        private String EMAIL = "";
        private String EMAIL_INVITE = "";
        private String PHONE = "";
        private bool ISDECAPTCHA = false;
        private bool ISPLAYPK = true;
        private int TYPE = 1;
        private BackgroundWorker M_RESET;
        private BackgroundWorker M_HIDE;
        private BackgroundWorker M_GETCODE;
        private BackgroundWorker M_CAPTCHA;
        private const String URLLOGIN = "https://prod.cashkinggame.com/CKService.svc/v3.2/login/?{0}";
        private String UDID = "e259dd301679a99ee96c30fd91f95d1dd57fd60c";
        private String BUSINESSTOKEN = "AbxmN8H66m58KZ6b";
        private String URL_REG_FACEBOOK = "https://m.facebook.com/r.php";
        private String URL_FACEBOOK = "https://m.facebook.com";
        private String PASS = "admin123";
        private String URL_INVITE = "https://www.facebook.com/v2.2/dialog/apprequests?access_token={0}&app_id=163291417175382&channel=https%3A%2F%2Fs-static.ak.facebook.com%2Fconnect%2Fxd_arbiter%2F4B2NplaqNF3.js%3Fversion%3D41%23cb%3Dfc6fe800325fbe%26domain%3Dd37p16zsmx53qq.cloudfront.net%26origin%3Dhttps%253A%252F%252Fd37p16zsmx53qq.cloudfront.net%252Ff3d768e98910cbc%26relation%3Dparent.parent&channel_url=https%3A%2F%2Fs-static.ak.facebook.com%2Fconnect%2Fxd_arbiter%2F4B2NplaqNF3.js%3Fversion%3D41%23cb%3Df3ea21df7bd97c%26domain%3Dd37p16zsmx53qq.cloudfront.net%26origin%3Dhttps%253A%252F%252Fd37p16zsmx53qq.cloudfront.net%252Ff3d768e98910cbc%26relation%3Dparent.parent&data=invite&display=iframe&e2e=%7B%7D&exclude_ids=&filters=null&frictionless=true&locale=en_US&message=Come%20and%20play%20Pirate%20Kings!&next=https%3A%2F%2Fs-static.ak.facebook.com%2Fconnect%2Fxd_arbiter%2F4B2NplaqNF3.js%3Fversion%3D41%23cb%3Df2cee3219e55944%26domain%3Dd37p16zsmx53qq.cloudfront.net%26origin%3Dhttps%253A%252F%252Fd37p16zsmx53qq.cloudfront.net%252Ff3d768e98910cbc%26relation%3Dparent%26frame%3Df133db584324ca2%26result%3D%2522xxRESULTTOKENxx%2522&sdk=joey&title=Come%20play%20Pirate%20Kings!&to=AVnlf4UjvCgD6J3lib7Punii493TvhIWwEPLWGNnWyZXQnFjRh6BeYNELsDMfJyysoTerCe0B_QT2Ctjlri47PU8h-P8-U-DmeV3zB-JSGIkAg&version=v2.2";
        //private String URL_ACCEPT_PK = "https://www.facebook.com/v2.2/dialog/oauth?app_id=163291417175382&client_id=163291417175382&display=popup&domain=d37p16zsmx53qq.cloudfront.net&e2e=%7B%7D&locale=en_US&origin=1&redirect_uri=https%3A%2F%2Fs-static.ak.facebook.com%2Fconnect%2Fxd_arbiter%2F4B2NplaqNF3.js%3Fversion%3D41%23cb%3Df1b9010d1dc670e%26domain%3Dd37p16zsmx53qq.cloudfront.net%26origin%3Dhttps%253A%252F%252Fd37p16zsmx53qq.cloudfront.net%252Fff7e54c3ab653a%26relation%3Dopener%26frame%3Df91c966b33a1ea&response_type=token%2Csigned_request&scope=user_friends%2Cemail%2Cpublish_actions%2Cpublic_profile&sdk=joey&version=v2.2";
        private String URL_ACCEPT_PK = "https://www.facebook.com/v2.2/dialog/oauth?redirect_uri=https%3A%2F%2Fs-static.ak.facebook.com%2Fconnect%2Fxd_arbiter%2Fjb3BUxkAISL.js%3Fversion%3D41%23cb%3Df32907de2ec2708%26domain%3Dd37p16zsmx53qq.cloudfront.net%26origin%3Dhttps%253A%252F%252Fd37p16zsmx53qq.cloudfront.net%252Ff374ddc02be06%26relation%3Dopener%26frame%3Df323e7f55c99c5c&display=popup&scope=user_friends%2Cemail%2Cpublish_actions%2Cpublic_profile&response_type=token%2Csigned_request&domain=d37p16zsmx53qq.cloudfront.net&auth_type=rerequest&origin=1&client_id=163291417175382&ret=login&sdk=joey&ext=1445915286&hash=AebmIeiT8pvx_UO9";
        private bool ISSTOP_REG = false;
        private bool ISSTOP_PLAY = false;
        private String FBIDS = String.Empty;
        #endregion

        #region "EVENTS ON FORM"
        /// <summary>
        /// 
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
            //
            M_RESET = new BackgroundWorker();
            M_RESET.DoWork += new DoWorkEventHandler(m_Reset_DoWork);
            M_RESET.ProgressChanged += new ProgressChangedEventHandler(m_Reset_ProgressChanged);
            M_RESET.RunWorkerCompleted += new RunWorkerCompletedEventHandler(m_Reset_RunWorkerCompleted);
            M_RESET.WorkerReportsProgress = true;
            M_RESET.WorkerSupportsCancellation = true;
            //
            M_HIDE = new BackgroundWorker();
            M_HIDE.DoWork += new DoWorkEventHandler(m_Hide_DoWork);
            M_HIDE.ProgressChanged += new ProgressChangedEventHandler(m_Hide_ProgressChanged);
            M_HIDE.RunWorkerCompleted += new RunWorkerCompletedEventHandler(m_Hide_RunWorkerCompleted);
            M_HIDE.WorkerReportsProgress = true;
            M_HIDE.WorkerSupportsCancellation = true;
            //
            M_GETCODE = new BackgroundWorker();
            M_GETCODE.DoWork += new DoWorkEventHandler(m_GetCode_DoWork);
            M_GETCODE.ProgressChanged += new ProgressChangedEventHandler(m_GetCode_ProgressChanged);
            M_GETCODE.RunWorkerCompleted += new RunWorkerCompletedEventHandler(m_GetCode_RunWorkerCompleted);
            M_GETCODE.WorkerReportsProgress = true;
            M_GETCODE.WorkerSupportsCancellation = true;
            //
            M_CAPTCHA = new BackgroundWorker();
            M_CAPTCHA.DoWork += new DoWorkEventHandler(m_Captcha_DoWork);
            M_CAPTCHA.ProgressChanged += new ProgressChangedEventHandler(m_Captcha_ProgressChanged);
            M_CAPTCHA.RunWorkerCompleted += new RunWorkerCompletedEventHandler(m_Captcha_RunWorkerCompleted);
            M_CAPTCHA.WorkerReportsProgress = true;
            M_CAPTCHA.WorkerSupportsCancellation = true;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //
            checkAutoUpdate();
            //
            ISDECAPTCHA = chkDecaptcha.Checked;
            //
            getListAccount();
            //
            geckoWebBrowser.Navigate(URL_REG_FACEBOOK);
            //
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                geckoWebBrowser.Dispose();
                Xpcom.Shutdown();
            }
            catch
            {
            
            }
        }
        #endregion

        #region "METHODS"
        private void checkAutoUpdate()
        {
            //Uncomment below line to see Russian version

            //AutoUpdater.CurrentCulture = CultureInfo.CreateSpecificCulture("ru-RU");

            //If you want to open download page when user click on download button uncomment below line.

            //AutoUpdater.OpenDownloadPage = true;

            //Don't want user to select remind later time in AutoUpdater notification window then uncomment 3 lines below so default remind later time will be set to 2 days.

            //AutoUpdater.LetUserSelectRemindLater = false;
            //AutoUpdater.RemindLaterTimeSpan = RemindLaterFormat.Days;
            //AutoUpdater.RemindLaterAt = 2;

            AutoUpdater.Start("http://222.255.29.210:3007/pk/lnt.xml");
        
        }
        /// <summary>
        /// 
        /// </summary>
        private void fillLoginInviteFB()
        {

            var account = (from acc in ACCOUNTSFB
                           where acc.Done == false
                           select acc).FirstOrDefault();
            if (account == null)
            {
                lblMsg.Text = "All account invited.";
                return;
            }
            EMAIL_INVITE = account.Email;
            lblMsg.Text = String.Format("[INVITE] From nick FB: {0}", EMAIL_INVITE);
            GeckoDocument document = (GeckoDocument)geckoWebBrowser.Window.Document;
            GeckoHtmlElement email = document.GetElementsByName("email").FirstOrDefault();
            email.SetAttribute("value", account.Email);

            GeckoHtmlElement pass = document.GetElementsByName("pass").FirstOrDefault();
            pass.SetAttribute("value", account.Phone);

            //login
            var login = (GeckoInputElement)document.GetElementsByName("login").FirstOrDefault(); ;

            login.Click();
        }

        /// <summary>
        /// 
        /// </summary>
        private void fillLoginFB()
        {
            //
            var account = (from acc in ACCOUNTS
                           where acc.Done == false
                           select acc).FirstOrDefault();
            if (account == null)
            {
                lblMsg.Text = "All account invited.";
                btnLoginFB.Text = "Login FB";
                ISSTOP_PLAY = false;
                return;
            }
            EMAIL = account.Email;
            PHONE = account.Phone;
            lblMsg.Text = String.Format("[LOGIN] Email: {0} Phone {1}", EMAIL, PHONE);
            GeckoDocument document = (GeckoDocument)geckoWebBrowser.Window.Document;
            GeckoHtmlElement email = document.GetElementsByName("email").FirstOrDefault();
            email.SetAttribute("value", account.Phone);

            GeckoHtmlElement pass = document.GetElementsByName("pass").FirstOrDefault();
            pass.SetAttribute("value", PASS);

            //login
            var login = (GeckoInputElement)document.GetElementsByName("login").FirstOrDefault(); ;

            login.Click();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fbId"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        private string getDataLogin(String fbId, String name, String accessToken)
        {
            Dictionary<string, object> dicResult = new Dictionary<string, object>();
            dicResult.Add("CampaignReferral", "");
            dicResult.Add("DeviceToken", null);
            dicResult.Add("Email", null);
            dicResult.Add("FBID", fbId);
            dicResult.Add("FBName", name);
            dicResult.Add("FriendFBIDs", JsonConvert.SerializeObject(new List<String>()));
            dicResult.Add("GCID", null);
            dicResult.Add("GameVersion", 215);
            dicResult.Add("Platform", 2);
            dicResult.Add("UDID", UDID);
            dicResult.Add("BusinessToken", BUSINESSTOKEN);
            dicResult.Add("AccessToken", accessToken);
            return JsonConvert.SerializeObject(dicResult);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="isDone"></param>
        private void updateDone(String email, bool isDone)
        {
            foreach (Acc acc in ACCOUNTS)
            {
                if (acc.Email == email)
                {
                    acc.Done = isDone;
                    break;
                }
            }
            saveFileAcc();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="isUsed"></param>
        private void updateSatatus(String email, bool isUsed)
        {
            foreach (Acc acc in ACCOUNTS)
            {
                if (acc.Email == email)
                {
                    acc.Used = isUsed;
                    break;
                }
            }
            saveFileAcc();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="mobile"></param>
        private void resetMobile(String email, String mobile)
        {
            foreach (Acc acc in ACCOUNTS)
            {
                if (acc.Email == email)
                {
                    acc.Phone = mobile;
                    acc.Used = false;
                    acc.Done = false;
                    break;
                }
            }
            saveFileAcc();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="isUsed"></param>
        private void updateMobile(String email, String mobile)
        {
            foreach (Acc acc in ACCOUNTS)
            {
                if (acc.Email == email)
                {
                    acc.Phone = mobile;
                    break;
                }
            }
            saveFileAcc();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        private void hideChatGroup(String email)
        {
            try
            {
                String url = String.Format(URL, "5", email);
                String ret = doGet(url);
            }
            catch
            {
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private String getReset(String email)
        {
            try
            {
                String url = String.Format(URL, "3", email);
                String ret = doGet(url);
                JToken jToken = JObject.Parse(ret);
                if (jToken["tptn"] != null)
                    ret = "+1" + jToken["tptn"].ToString();
                return ret;
            }
            catch
            {
                return String.Empty;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private String getMobile(String email)
        {
            try
            {
                String url = String.Format(URL, "1", email);
                String ret = doGet(url);
                return ret;
            }
            catch
            {
                return String.Empty;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private List<String> getMessage(String email)
        {
            List<String> lstMsg = new List<string>();
            try
            {
                String url = String.Format(URL, "2", email);
                String ret = doGet(url);
                JToken jToken = JObject.Parse(ret);
                if (jToken["conversations"]["conversation"].Count() == 1)
                {
                    lstMsg.Add(jToken["conversations"]["conversation"]["@attributes"]["message"].ToString());
                }
                else
                {
                    //Get lastest message
                    foreach (JToken token in jToken["conversations"]["conversation"])
                    {
                        if (token["@attributes"]["message"] != null)
                        {
                            lstMsg.Add(token["@attributes"]["message"].ToString());
                            break;
                        }
                    }
                }
                return lstMsg;
            }
            catch
            {
                return lstMsg;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private String getCode(String msg)
        {
            try
            {
                Regex re = new Regex(@"\d+");
                Match m = re.Match(msg);
                if (m.Success)
                {
                    return m.Value.ToString();
                }
                else
                {
                    return String.Empty;
                }
            }
            catch
            {
                return String.Empty;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private String doGet(string url)
        {
            try
            {
                System.Net.ServicePointManager.Expect100Continue = false;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);

                string data = reader.ReadToEnd();

                reader.Close();
                stream.Close();

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        private void saveFileAcc()
        {
            try
            {
                if (ACCOUNTS == null || ACCOUNTS.Count() == 0) return;
                String content = JsonConvert.SerializeObject(ACCOUNTS);
                File.WriteAllText(FILENAME_FBIDS, content);
            }
            catch
            {

            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void saveFileFbids()
        {
            try
            {
                File.WriteAllText(FILENAME_FBIDS_OUT, String.Empty);
            }
            catch
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fbid"></param>
        private void saveFileFbids(String fbid)
        {
            try
            {
                String content = MyFile.ReadFile(FILENAME_FBIDS_OUT);
                if (content.Trim() == String.Empty)
                {
                    File.AppendAllText(FILENAME_FBIDS_OUT, String.Format("{0}", fbid));
                }
                else
                {
                    File.AppendAllText(FILENAME_FBIDS_OUT, String.Format(",{0}", fbid));
                }

            }
            catch
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void getListAccount()
        {
            try
            {
                String content = MyFile.ReadFile(FILENAME_FBIDS);
                ACCOUNTS = JsonConvert.DeserializeObject<List<Acc>>(content);
                var account = (from acc in ACCOUNTS
                               where acc.Used == false
                               select acc).FirstOrDefault();
                if (account != null)
                {
                    EMAIL = ACCOUNTS[0].Email;
                    PHONE = ACCOUNTS[0].Phone;
                }

                //
                content = MyFile.ReadFile(FILENAME_FBS);
                ACCOUNTSFB = JsonConvert.DeserializeObject<List<Acc>>(content);
            }
            catch
            {

            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void fillInfoFBReset()
        {
            lblMsg.Text = String.Format("[REG] Email: {0} Phone {1}", EMAIL, PHONE);
            GeckoDocument document = (GeckoDocument)geckoWebBrowser.Window.Document;

            //
            var rand = new Random();
            Int32 intGender = rand.Next(1, 2);
            //
            Gender eGender = Gender.Male;
            switch (intGender)
            {
                case 1:
                    eGender = Gender.Female;
                    break;
                case 2:
                    eGender = Gender.Male;
                    break;
                default:
                    break;
            }

            GeckoHtmlElement firstName = document.GetElementsByName("firstname").FirstOrDefault();
            firstName.SetAttribute("value", NameGenerator.GenerateFirstName(eGender));

            GeckoHtmlElement lastName = document.GetElementsByName("lastname").FirstOrDefault();
            lastName.SetAttribute("value", NameGenerator.GenerateLastName());

            GeckoHtmlElement email = document.GetElementsByName("email").FirstOrDefault();
            email.SetAttribute("value", PHONE);

            //gender
            GeckoSelectElement gender = (GeckoSelectElement)document.GetElementById("gender");
            gender.SelectedIndex = intGender;

            //day
            GeckoHtmlElement day = document.GetElementsByName("day").FirstOrDefault();
            day.SetAttribute("value", "30");
            //month
            GeckoHtmlElement month = document.GetElementsByName("month").FirstOrDefault();
            month.SetAttribute("value", "12");
            //year
            GeckoHtmlElement year = document.GetElementsByName("year").FirstOrDefault();
            year.SetAttribute("value", "1985");
            //pass
            GeckoHtmlElement pass = document.GetElementsByName("pass").FirstOrDefault();
            pass.SetAttribute("value", PASS);

            var signup_button = (GeckoInputElement)document.GetElementById("signup_button");

            signup_button.Click();
        }

        /// <summary>
        /// 
        /// </summary>
        private void fillInfoFB()
        {
            //
            var account = (from acc in ACCOUNTS
                           where acc.Used == false
                           select acc).FirstOrDefault();
            if (account == null)
            {
                lblMsg.Text = "All account are used.";
                btnRegFB.Text = "Reg FB";
                ISSTOP_REG = false;
                return;
            }
            //
            String mobile = account.Phone;
            EMAIL = account.Email;
            if (mobile == String.Empty)
            {
                Task<String> taskMobile = Task.Factory.StartNew(() => getMobile(EMAIL));
                taskMobile.Wait();
                mobile = taskMobile.Result;
            }
            PHONE = mobile;
            //
            updateMobile(EMAIL, mobile);
            //
            lblMsg.Text = String.Format("[REG] Email: {0} Phone {1}", EMAIL, mobile);
            GeckoDocument document = (GeckoDocument)geckoWebBrowser.Window.Document;

            //
            var rand = new Random();
            Int32 intGender = rand.Next(1, 2);
            //
            Gender eGender = Gender.Male;
            switch (intGender)
            {
                case 1:
                    eGender = Gender.Female;
                    break;
                case 2:
                    eGender = Gender.Male;
                    break;
                default:
                    break;
            }

            GeckoHtmlElement firstName = document.GetElementsByName("firstname").FirstOrDefault();
            firstName.SetAttribute("value", NameGenerator.GenerateFirstName(eGender));

            GeckoHtmlElement lastName = document.GetElementsByName("lastname").FirstOrDefault();
            lastName.SetAttribute("value", NameGenerator.GenerateLastName());

            GeckoHtmlElement email = document.GetElementsByName("email").FirstOrDefault();
            email.SetAttribute("value", PHONE);

            //gender
            GeckoSelectElement gender = (GeckoSelectElement)document.GetElementById("gender");
            gender.SelectedIndex = intGender;

            //day
            GeckoHtmlElement day = document.GetElementsByName("day").FirstOrDefault();
            day.SetAttribute("value", "30");
            //month
            GeckoHtmlElement month = document.GetElementsByName("month").FirstOrDefault();
            month.SetAttribute("value", "12");
            //year
            GeckoHtmlElement year = document.GetElementsByName("year").FirstOrDefault();
            year.SetAttribute("value", "1985");
            //pass
            GeckoHtmlElement pass = document.GetElementsByName("pass").FirstOrDefault();
            pass.SetAttribute("value", PASS);

            var signup_button = (GeckoInputElement)document.GetElementById("signup_button");

            signup_button.Click();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        private String getAccessToken(String head)
        {
            try
            {
                Regex reg = new Regex(@"(?<access_token>access_token=*.*)&expires_in");
                MatchCollection mc = reg.Matches(head);
                if (mc.Count == 0) return String.Empty;
                return mc[0].Groups["access_token"].Value.ToString().Replace("access_token=", "").Trim();
            }
            catch
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string doPost(string uri, string parameters)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                System.Net.ServicePointManager.Expect100Continue = false;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                byte[] bytes = Encoding.UTF8.GetBytes(parameters);
                request.ContentLength = bytes.Length;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);

                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);

                var result = reader.ReadToEnd();
                stream.Dispose();
                reader.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        private void removeCookie()
        {
            nsICookieManager CookieMan;
            CookieMan = Xpcom.GetService<nsICookieManager>("@mozilla.org/cookiemanager;1");
            CookieMan = Xpcom.QueryInterface<nsICookieManager>(CookieMan);
            CookieMan.RemoveAll();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="proxy"></param>
        private void setGeckoPreferences(Proxy proxy)
        {
            GeckoPreferences.Default["network.proxy.type"] = 1;
            //GeckoPreferences.Default["network.proxy.http"] = proxy.Host;
            //GeckoPreferences.Default["network.proxy.http_port"] = Convert.ToInt32(proxy.Port);
            GeckoPreferences.Default["network.proxy.ssl"] = proxy.Host;
            GeckoPreferences.Default["network.proxy.ssl_port"] = Convert.ToInt32(proxy.Port);
        }
        #endregion

        #region "M_RESET"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_Reset_DoWork(object sender, DoWorkEventArgs e)
        {
            int type = (int) e.Argument;
            if (type == 1)
            {
                //fbids
                saveFileFbids();
                //ACCOUNTS
                foreach (Acc acc in ACCOUNTS)
                {
                    String mobile = getReset(acc.Email);
                    resetMobile(acc.Email, mobile);
                    String msg = String.Format("[RESET] Email: {0} Phone: {1}", acc.Email, mobile);
                    M_RESET.ReportProgress(50, msg);
                    if (M_RESET.CancellationPending)
                    {
                        e.Cancel = true;
                        M_RESET.ReportProgress(0);
                        return;
                    }
                }
                e.Result = 1;
                M_RESET.ReportProgress(100);
            }
            else if (type == 2)
            {
                String msg = String.Format("[RESET] Email: {0}", EMAIL);
                M_RESET.ReportProgress(50, msg);
                String mobile = getReset(EMAIL);
                updateMobile(EMAIL, mobile);
                msg = String.Format("[RESET] Email: {0} Phone: {1}", EMAIL, mobile);
                M_RESET.ReportProgress(50, msg);
                PHONE = mobile;
                STEP = -1;
                e.Result = 2;
                M_RESET.ReportProgress(100);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_Reset_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                String msg = e.UserState.ToString();
                lblMsg.Text = msg;
            }
            catch
            {
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_Reset_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                btnResetAll.Text = "Reset";
                return;
            }
            // Check to see if an error occurred in the background process.
            else if (e.Error != null)
            {
                MessageBox.Show("Error.", "AutoRegFB", MessageBoxButtons.OK);
            }
            else
            {
                if (e.Result == null) return;
                int type = (int) e.Result;
                if (type == 1)
                {
                    MessageBox.Show("Reset all done.", "AutoRegFB", MessageBoxButtons.OK);
                    btnResetAll.Text = "Reset";
                }
                else if (type == 2)
                {
                    geckoWebBrowser.Navigate(URL_REG_FACEBOOK);
                }
            }
        }
        #endregion

        #region "M_HIDE"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_Hide_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (Acc acc in ACCOUNTS)
            {
                String msg = String.Format("[HIDECHATGROUP] Email: {0}", acc.Email);
                hideChatGroup(acc.Email);
                M_HIDE.ReportProgress(50, msg);
                if (M_HIDE.CancellationPending)
                {
                    e.Cancel = true;
                    M_HIDE.ReportProgress(0);
                    return;
                }
            }
            M_HIDE.ReportProgress(100);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_Hide_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                String msg = e.UserState.ToString();
                lblMsg.Text = msg;
            }
            catch
            {
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_Hide_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                btnCleanMsg.Text = "Clean Message";
                return;
            }
            // Check to see if an error occurred in the background process.
            else if (e.Error != null)
            {
                MessageBox.Show("Error.", "AutoRegFB", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Hide chat group all done.", "AutoRegFB", MessageBoxButtons.OK);
                btnCleanMsg.Text = "Clean Message";
            }
        }
        #endregion

        #region "M_GETCODE"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_GetCode_DoWork(object sender, DoWorkEventArgs e)
        {
            Dictionary<string, object> dic = (Dictionary<string, object>) e.Argument;
            String msg = "[GETPINCODE] Working... Email: " + EMAIL;
            M_GETCODE.ReportProgress(50, msg);
            String pin = getCode(getMessage(EMAIL)[0]);
            msg = "[GETPINCODE] Result: " + pin;
            M_GETCODE.ReportProgress(50, msg);
            dic.Add("value", pin);
            e.Result = dic;
            M_GETCODE.ReportProgress(100);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_GetCode_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                String msg = e.UserState.ToString();
                lblMsg.Text = msg;
            }
            catch
            {
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_GetCode_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }
            else
            {
                if (e.Result == null) return;
                Dictionary<string, object> dic = (Dictionary<string, object>)e.Result;
                String pinCode = (String)dic["value"];
                if (pinCode != String.Empty)
                {
                    lblMsg.Text = String.Format("[REG] Email: {0} Phone {1}", EMAIL, PHONE);
                    var input = (GeckoHtmlElement)dic["input"];
                    input.SetAttribute("value", pinCode);
                    var submit = (GeckoHtmlElement)dic["submit"];
                    submit.Click();
                }
                else
                {
                    MessageBox.Show("Error get pin code!", "AutoRegFB", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region "M_CAPTCHA"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_Captcha_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument == null) return;
            Dictionary<string, object> dic = (Dictionary<string, object>)e.Argument;
            byte[] imageByteArray = (byte[]) dic["imageByteArray"];
            String msg = "[DECAPTCHA] Working...";
            M_CAPTCHA.ReportProgress(50, msg);
            String captcha = getCaptchaCode(imageByteArray);
            if (captcha == null) captcha = String.Empty;
            msg = "[DECAPTCHA] Result: " + captcha;
            M_CAPTCHA.ReportProgress(50, msg);
            dic.Add("value", captcha);
            e.Result = dic;
            if (M_CAPTCHA.CancellationPending)
            {
                e.Cancel = true;
                M_CAPTCHA.ReportProgress(0);
                return;
            }
            M_CAPTCHA.ReportProgress(100);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_Captcha_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                String msg = e.UserState.ToString();
                lblMsg.Text = msg;
            }
            catch
            {
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_Captcha_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                return;
            }
            else if (e.Error != null)
            {
                return;
            }
            else
            {
                if (e.Result == null) return;
                Dictionary<string, object> dic = (Dictionary<string, object>)e.Result;
                String captcha = (String) dic["value"];
                if (captcha != String.Empty)
                {
                    if (TYPE == 1)
                    {
                        lblMsg.Text = String.Format("[REG] Email: {0} Phone {1}", EMAIL, PHONE);
                    }
                    else
                    {
                        lblMsg.Text = String.Format("[PLAY] Email: {0} Phone {1}", EMAIL, PHONE);
                    }
                    var input = (GeckoHtmlElement)dic["input"];
                    input.SetAttribute("value", captcha);
                    var submit = (GeckoHtmlElement)dic["submit"];
                    submit.Click();
                }
                else
                {
                    MessageBox.Show("Error get captcha!", "AutoRegFB", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region "EVENTS OF CONTROLS"

        private void btnInvite_Click(object sender, EventArgs e)
        {
            //Reset
            foreach (Acc acc in ACCOUNTSFB)
            {
                acc.Done = false;
            }
            //
            lblMsg.Text = String.Empty;
            FBIDS = MyFile.ReadFile(FILENAME_FBIDS_OUT).Trim();
            if (FBIDS == String.Empty)
            {
                lblMsg.Text = String.Format("[INVITE] FBIDS is empty.");
                return;
            }
            if (geckoWebBrowser.Url.AbsoluteUri != URL_FACEBOOK)
            {
                geckoWebBrowser.Navigate(URL_FACEBOOK);
                TIMER_INVITE.Interval = 2000;
                TIMER_INVITE.Enabled = true;
                TIMER_INVITE.Tick += new System.EventHandler(this.timer_Invite_Tick);
            }
            else
            {
                TYPE = 3;
                STEP = 1;
                fillLoginInviteFB();
            }
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            try
            {
                GeckoDocument document = (GeckoDocument)geckoWebBrowser.Window.Document;
                var logout = getGeckoHtmlElementLogout(document);
                if (logout != null)
                {
                    //Lưu thông tin
                    String cookie = document.Cookie;
                    if (cookie != String.Empty)
                    {
                        String[] arrCookie = cookie.Split(';');
                        foreach (String cook in arrCookie)
                        {
                            if (cook.Trim().StartsWith("c_user="))
                            {
                                String fbid = cook.Replace("c_user=", "").Trim();
                                if (fbid != String.Empty)
                                {
                                    saveFileFbids(fbid);
                                }
                                break;
                            }
                        }
                    }
                    //
                    updateSatatus(EMAIL, true);
                    //
                    STEP = 2;
                    //logout
                    logout.Click();
                    return;
                }
            }
            catch
            {
            }
        }
        private void btnGetPinCode_Click(object sender, EventArgs e)
        {
            try
            {
                GeckoDocument document = (GeckoDocument)geckoWebBrowser.Window.Document;
                var input = getGeckoHtmlElementInput(document);
                var submit = getGeckoHtmlElementSubmit(document);
                try
                {
                    M_GETCODE.CancelAsync();
                }
                catch
                { 
                
                }
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("input", input);
                dic.Add("submit", submit);
                M_GETCODE.RunWorkerAsync(dic);

            }
            catch
            { 
            
            }
        }
        private void btnLoginFB_Click(object sender, EventArgs e)
        {
            try
            {
                String btnText = btnLoginFB.Text;
                if (btnText == "Stop")
                {
                    btnLoginFB.Text = "Login FB";
                    ISSTOP_PLAY = true;
                }
                else if (btnText == "Login FB")
                {
                    if (ACCOUNTS == null || ACCOUNTS.Count == 0) return;
                    if (MessageBox.Show("Are you sure to continue?", "AutoRegFB", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                    btnLoginFB.Text = "Stop";
                    ISSTOP_PLAY = false;
                    if (geckoWebBrowser.Url.AbsoluteUri != URL_FACEBOOK)
                    {
                        geckoWebBrowser.Navigate(URL_FACEBOOK);
                        TIMER_PLAY.Interval = 2000;
                        TIMER_PLAY.Enabled = true;
                        TIMER_PLAY.Tick += new System.EventHandler(this.timer_Play_Tick);
                    }
                    else
                    {
                        TYPE = 2;
                        STEP = 1;
                        fillLoginFB();
                    }
                }
            }
            catch
            {

            }
        }

        private void chk_CheckedChanged(object sender, EventArgs e)
        {
            ISDECAPTCHA = chkDecaptcha.Checked;
            try
            {
                if (!ISDECAPTCHA)
                {
                    M_CAPTCHA.CancelAsync();
                }
            }
            catch
            { 
            
            }

        }

        private void btnOpenFBIDS_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(FILENAME_FBIDS_OUT);
            }
            catch
            {

            }

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                GeckoDocument document = (GeckoDocument)geckoWebBrowser.Window.Document;
                String html = document.Body.Parent.OuterHtml;
                File.WriteAllText(FILENAME_FILE_HTML_SAVE, html);
                try
                {
                    Process.Start(FILENAME_FILE_HTML_SAVE);
                }
                catch
                {

                }
            }
            catch
            {

            }
        }
        private void btnRegFB_Click(object sender, EventArgs e)
        {
            String btnText = btnRegFB.Text;
            if (btnText == "Stop")
            {
                btnRegFB.Text = "Reg FB";
                ISSTOP_REG = true;
            }
            else if (btnText == "Reg FB")
            {
                btnRegFB.Text = "Stop";
                ISSTOP_REG = false;
                if (geckoWebBrowser.Url.AbsoluteUri != URL_REG_FACEBOOK)
                {
                    geckoWebBrowser.Navigate(URL_REG_FACEBOOK);
                    TIMER_REG.Interval = 2000;
                    TIMER_REG.Enabled = true;
                    TIMER_REG.Tick += new System.EventHandler(this.timer_Reg_Tick);
                }
                else
                {
                    TYPE = 1;
                    STEP = 1;
                    fillInfoFB();
                }
            }
        }
        private void btnResetAll_Click(object sender, EventArgs e)
        {
            try
            {
                String txtBtn = btnResetAll.Text;
                switch (txtBtn.ToUpper())
                {
                    case "RESET":
                        btnResetAll.Text = "Cancel";
                        M_RESET.RunWorkerAsync(1);
                        break;
                    case "CANCEL":
                        M_RESET.CancelAsync();
                        break;
                    default:
                        break;
                }
            }
            catch
            {

            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (EMAIL == String.Empty) return;
                nsICookieManager CookieMan;
                CookieMan = Xpcom.GetService<nsICookieManager>("@mozilla.org/cookiemanager;1");
                CookieMan = Xpcom.QueryInterface<nsICookieManager>(CookieMan);
                CookieMan.RemoveAll();
                //
                M_RESET.RunWorkerAsync(2);
            }
            catch
            {

            }
        }

        private void btnCleanMsg_Click(object sender, EventArgs e)
        {
            try
            {
                String txtBtn = btnCleanMsg.Text;
                switch (txtBtn.ToUpper())
                {
                    case "CLEAN MESSAGE":
                        btnCleanMsg.Text = "Cancel";
                        M_HIDE.RunWorkerAsync();
                        break;
                    case "CANCEL":
                        M_HIDE.CancelAsync();
                        break;
                    default:
                        break;
                }
            }
            catch
            {

            }
        }
        #endregion

        #region "EVENTS OF GECKOFX"
        private void geckoWebBrowser_DocumentCompleted(object sender, Gecko.Events.GeckoDocumentCompletedEventArgs e)
        {
            try
            {
                if (STEP == 0)
                {
                    //On first load
                    STEP = 1;
                    return;
                }
                if (TYPE == 1)
                {
                    if (STEP == -1)
                    {
                        STEP = 1;
                        fillInfoFBReset();
                        return;
                    }
                    if (STEP == 2)
                    {
                        STEP = 1;
                        var account = (from acc in ACCOUNTS
                                       where acc.Used == false
                                       select acc).FirstOrDefault();
                        if (account == null)
                        {
                            lblMsg.Text = "All account are used.";
                            btnRegFB.Text = "Reg FB";
                            ISSTOP_REG = false;
                            return;
                        }
                        if (!ISSTOP_REG)
                        {
                            TIMER_REG.Interval = 2000;
                            TIMER_REG.Enabled = true;
                            TIMER_REG.Tick += new System.EventHandler(this.timer_Reg_Tick);
                        }
                        geckoWebBrowser.Navigate(URL_REG_FACEBOOK);
                        return;
                    }
                    GeckoDocument document = (GeckoDocument)geckoWebBrowser.Window.Document;
                    GeckoHtmlElement captcha = (GeckoHtmlElement)document.GetElementsByName("captcha_response").FirstOrDefault();
                    String typeCaptcha = "hidden";
                    if (captcha != null) typeCaptcha = captcha.GetAttribute("type");

                    //Fake name
                    GeckoHtmlElement helpFackeName = getGeckoHtmlElementHelp(document);
                    if (helpFackeName != null)
                    {
                        GeckoSelectElement gender = (GeckoSelectElement)document.GetElementById("gender");
                        Int32 intGender = gender.SelectedIndex;
                        //
                        Gender eGender = Gender.Male;
                        switch (intGender)
                        {
                            case 1:
                                eGender = Gender.Female;
                                break;
                            case 2:
                                eGender = Gender.Male;
                                break;
                            default:
                                break;
                        }

                        GeckoHtmlElement firstName = document.GetElementsByName("firstname").FirstOrDefault();
                        firstName.SetAttribute("value", NameGenerator.GenerateFirstName(eGender));
                        GeckoHtmlElement lastName = document.GetElementsByName("lastname").FirstOrDefault();
                        lastName.SetAttribute("value", NameGenerator.GenerateLastName());

                        var signup_button = (GeckoInputElement)document.GetElementById("signup_button");
                        signup_button.Click();
                        return;
                    }

                    //Captcha
                    if (captcha != null && typeCaptcha != "hidden")
                    {
                        if (ISDECAPTCHA)
                        {
                            ImageCreator imageCreator = new ImageCreator(geckoWebBrowser);
                            GeckoImageElement imgCaptcha = (GeckoImageElement)document.Images[1];
                            byte[] imageByteArray = imageCreator.CanvasGetPngImage((uint)imgCaptcha.OffsetLeft, (uint)imgCaptcha.OffsetTop, (uint)imgCaptcha.OffsetWidth, (uint)imgCaptcha.OffsetHeight);
                            var submit = (GeckoHtmlElement)document.GetElementsByName("captcha_submit_text").FirstOrDefault();
                            if (submit == null) submit = (GeckoHtmlElement)document.GetElementById("u_0_0");
                            Dictionary<string, object> dic = new Dictionary<string, object>();
                            dic.Add("input", captcha);
                            dic.Add("submit", submit);
                            dic.Add("imageByteArray", imageByteArray);
                            M_CAPTCHA.RunWorkerAsync(dic);
                            return;
                        }
                        captcha.Focus();
                        return;
                    }

                    //ConfirmCode, invalid
                    GeckoHtmlElement sendConfirmCode = getGeckoHtmlElementSendConfirmCode(document);
                    GeckoHtmlElement invalidElement = getGeckoHtmlElementInvalid(document);
                    if (sendConfirmCode != null || invalidElement != null)
                    {
                        M_RESET.RunWorkerAsync(2);
                        return;
                    }

                    //Submit
                    var submission_request = (GeckoInputElement)document.GetElementsByName("submission_request").FirstOrDefault();
                    if (submission_request != null)
                    {
                        submission_request.Click();
                        return;
                    }

                    //Nhập số điện thoại, nhập mã code
                    GeckoHtmlElement inputEx = (GeckoHtmlElement)document.GetElementById("u_0_0");
                    if (inputEx != null)
                    {
                        String inputClass = inputEx.GetAttribute("class");
                        String inputName = inputEx.GetAttribute("name");
                        if (inputClass == "_5whq input")
                        {
                            var submit = getGeckoHtmlElementSubmit(document);
                            if (inputName == "contact_point")
                            {
                                inputEx.SetAttribute("value", PHONE);
                                submit.Click();
                                return;
                            }
                            else
                            {
                                Dictionary<string, object> dic = new Dictionary<string, object>();
                                dic.Add("input", inputEx);
                                dic.Add("submit", submit);
                                M_GETCODE.RunWorkerAsync(dic);
                                return;
                            }

                        }
                    }

                    //Nhập mã code
                    GeckoHtmlElement pin = document.GetElementsByName("pin").FirstOrDefault();
                    if (pin != null)
                    {
                        var submit = (GeckoHtmlElement)document.GetElementsByClassName("btn btnC").FirstOrDefault();
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic.Add("input", pin);
                        dic.Add("submit", submit);
                        M_GETCODE.RunWorkerAsync(dic);
                        return;
                    }

                    //Nhập mã code
                    GeckoHtmlElement code = getGeckoHtmlElementCode(document);
                    if (code != null)
                    {
                        var submit = getGeckoHtmlElementSubmit(document);
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic.Add("input", code);
                        dic.Add("submit", submit);
                        M_GETCODE.RunWorkerAsync(dic);
                        return;
                    }

                    //NextWizard
                    var nextWizard = getGeckoHtmlElementNextWizard(document);
                    if (nextWizard != null)
                    {
                        //Logout
                        var logout = getGeckoHtmlElementLogout(document);
                        if (logout != null)
                        {
                            //Lưu thông tin
                            String cookie = document.Cookie;
                            if (cookie != String.Empty)
                            {
                                String[] arrCookie = cookie.Split(';');
                                foreach (String cook in arrCookie)
                                {
                                    if (cook.Trim().StartsWith("c_user="))
                                    {
                                        String fbid = cook.Replace("c_user=", "").Trim();
                                        if (fbid != String.Empty)
                                        {
                                            saveFileFbids(fbid);
                                        }
                                        break;
                                    }
                                }
                            }
                            //
                            updateSatatus(EMAIL, true);
                            //
                            STEP = 2;
                            //logout
                            logout.Click();
                            return;
                        }
                        else
                        {
                            nextWizard.Click();
                            return;
                        }
                    }
                }
                else if (TYPE == 2)
                {
                    if (STEP == 10)
                    {
                        //removeCookie();
                        STEP = 1;
                        
                        var account = (from acc in ACCOUNTS
                                       where acc.Done == false
                                       select acc).FirstOrDefault();
                        if (account == null)
                        {
                            lblMsg.Text = "All account invited.";
                            btnLoginFB.Text = "Login FB";
                            ISSTOP_PLAY = false;
                            return;
                        }
                        if (!ISSTOP_PLAY)
                        {
                            TIMER_PLAY.Interval = 2000;
                            TIMER_PLAY.Enabled = true;
                            TIMER_PLAY.Tick += new System.EventHandler(this.timer_Play_Tick);
                            geckoWebBrowser.Navigate(URL_FACEBOOK);
                        }
                        return;
                    }
                    //
                    GeckoDocument document = (GeckoDocument)geckoWebBrowser.Window.Document;
                    GeckoHtmlElement captcha = (GeckoHtmlElement)document.GetElementsByName("captcha_response").FirstOrDefault();
                    String typeCaptcha = "hidden";
                    if (captcha != null) typeCaptcha = captcha.GetAttribute("type");

                    //Captcha
                    if (captcha != null && typeCaptcha != "hidden")
                    {
                        String idCaptcha = captcha.GetAttribute("id");
                        String classCaptcha = captcha.GetAttribute("class");
                        if (idCaptcha == "u_0_0" && classCaptcha == "_5whq input")
                        {
                            //Mã xác nhận
                            var submitSubmit = (GeckoHtmlElement)document.GetElementsByName("submit[Submit]").FirstOrDefault();
                            Dictionary<string, object> dic = new Dictionary<string, object>();
                            dic.Add("input", captcha);
                            dic.Add("submit", submitSubmit);
                            M_GETCODE.RunWorkerAsync(dic);
                        }
                        else
                        {
                            //Captcha
                            if (ISDECAPTCHA)
                            {
                                ImageCreator imageCreator = new ImageCreator(geckoWebBrowser);
                                GeckoImageElement imgCaptcha = (GeckoImageElement)document.Images[1];
                                byte[] imageByteArray = imageCreator.CanvasGetPngImage((uint)imgCaptcha.OffsetLeft, (uint)imgCaptcha.OffsetTop, (uint)imgCaptcha.OffsetWidth, (uint)imgCaptcha.OffsetHeight);
                                var submit = (GeckoHtmlElement)document.GetElementsByName("captcha_submit_text").FirstOrDefault();
                                if (submit == null) submit = (GeckoHtmlElement)document.GetElementById("u_0_0");
                                Dictionary<string, object> dic = new Dictionary<string, object>();
                                dic.Add("input", captcha);
                                dic.Add("submit", submit);
                                dic.Add("imageByteArray", imageByteArray);
                                M_CAPTCHA.RunWorkerAsync(dic);
                                return;
                            }
                            captcha.Focus();
                            return;
                        }
                    }

                    //photo_input
                    var photoInput = (GeckoHtmlElement)document.GetElementById("photo_input");
                    if (photoInput != null)
                    {
                        String type = photoInput.GetAttribute("type");
                        if (type != null && type == "file")
                        {
                            updateDone(EMAIL, true);
                            return;
                        }
                    }

                    //submit[Continue]
                    var submitContinue = (GeckoHtmlElement)document.GetElementsByName("submit[Continue]").FirstOrDefault();
                    if (submitContinue != null)
                    {
                        submitContinue.Click();
                        return;
                    }

                    //submit[Okay]
                    var submitOkay = (GeckoHtmlElement)document.GetElementsByName("submit[Okay]").FirstOrDefault();
                    if (submitOkay != null)
                    {
                        submitOkay.Click();
                        return;
                    }

                    //Accept play PK
                    if (ISPLAYPK)
                    {
                        var acceptPlay = getGeckoHtmlElementAcceptPlay(document);
                        if (acceptPlay != null)
                        {
                            acceptPlay.Click();
                            TIMER_ACCEPT.Interval = 2000;
                            TIMER_ACCEPT.Enabled = true;
                            TIMER_ACCEPT.Tick += new System.EventHandler(this.timer_Accept_Tick);
                            return;
                        }

                        String head = document.Head.InnerHtml;
                        String accessToken = getAccessToken(head);
                        if (accessToken != String.Empty)
                        {
                            String url = String.Format("https://graph.facebook.com/v2.2/me?access_token={0}", accessToken);
                            String ret = doGet(url);
                            if (ret != String.Empty)
                            {
                                JToken jToken = JObject.Parse(ret);
                                String fbid = jToken["id"].ToString();
                                String name = jToken["name"].ToString();
                                String reqLogin = getDataLogin(fbid, name, accessToken);
                                String urlLogin = String.Format(URLLOGIN, DateTime.Now.ToOADate().ToString());
                                String retLogin = doPost(urlLogin, reqLogin);
                                JToken jTokenLogin = JObject.Parse(retLogin);
                                String code = jTokenLogin["ErrorCode"].ToString();
                                lblMsg.Text = String.Format("[PLAY] FBID:{0} Code:{1}", fbid, code);
                            }
                            //
                            STEP = 3;
                            //
                            geckoWebBrowser.Navigate(URL_FACEBOOK);
                            return;
                        }
                    }
                    var logout = getGeckoHtmlElementLogout(document);
                    if (logout != null)
                    {
                        //Đăng nhập thành công
                        if (ISPLAYPK)
                        {
                            if (STEP == 1)
                            {
                                STEP = 2;
                                geckoWebBrowser.Navigate(URL_ACCEPT_PK);
                                return;
                            }
                            else if (STEP == 3)
                            {
                                updateDone(EMAIL, true);
                                STEP = 10;
                                logout.Click();
                                return;
                            }
                        }
                        else
                        {
                            updateDone(EMAIL, true);
                            STEP = 10;
                            logout.Click();
                            return;
                        }
                    }
                }
                else if (TYPE == 3)
                {
                    GeckoDocument document = (GeckoDocument)geckoWebBrowser.Window.Document;

                    String head = document.Head.InnerHtml;
                    String accessToken = getAccessToken(head);
                    if (accessToken != String.Empty)
                    {
                        STEP = 3;
                        geckoWebBrowser.Navigate(String.Format(URL_INVITE, accessToken));
                        return;
                    }
                    if (STEP == 6)
                    {
                        STEP = 1;
                        fillLoginInviteFB();
                        return;
                    }
                    if (STEP == 3)
                    {
                        GeckoHtmlElement to = getGeckoHtmlElementTo(document);
                        if (to != null)
                        {
                            to.SetAttribute("value", FBIDS);
                            GeckoHtmlElement sendRequest = getGeckoHtmlElementAcceptPlay(document);
                            if (sendRequest != null)
                            {
                                STEP = 4;
                                sendRequest.Click();
                                return;
                            }
                        }
                    }
                    if (STEP == 4)
                    {
                        STEP = 5;
                        geckoWebBrowser.Navigate(URL_FACEBOOK);
                        return;
                    }
                    var logout = getGeckoHtmlElementLogout(document);
                    if (logout != null)
                    {
                        if (STEP == 1)
                        {
                            STEP = 2;
                            geckoWebBrowser.Navigate(URL_ACCEPT_PK);
                            return;
                        }
                        else if (STEP == 5)
                        {
                            //
                            foreach (Acc acc in ACCOUNTSFB)
                            {
                                if (acc.Email == EMAIL_INVITE)
                                {
                                    acc.Done = true;
                                    break;
                                }
                            }
                            //
                            STEP = 6;
                            logout.Click();
                            return;
                        }
                    }
                
                }
            }
            catch
            {
                geckoWebBrowser.Refresh();
            }
        }
        #endregion

        #region "GECKOHTMLELEMENT"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private GeckoHtmlElement getGeckoHtmlElementInput(GeckoDocument document)
        {
            GeckoElementCollection nodes = document.GetElementsByTagName("input");
            if (nodes == null) return null;
            foreach (GeckoElement node in nodes)
            {
                String type = node.GetAttribute("type");
                if (type != "hidden") return (GeckoHtmlElement) node;
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private GeckoHtmlElement getGeckoHtmlElementInvalid(GeckoDocument document)
        {
            GeckoElementCollection nodes = document.GetElementsByTagName("span");
            if (nodes == null) return null;
            foreach (GeckoElement node in nodes)
            {
                String html = ((GeckoHtmlElement)node).InnerHtml;
                if (html != null && (html.Contains("bạn đã có tài khoản") || html.Contains("Không thể xác thực") || html.Contains("lòng thử lại số khác") || html.Contains("try a different number") || html.Contains("sử dụng một địa chỉ email hoặc số di động") || html.Contains("already in use by a registered account") || html.Contains("Could not validate your mobile number")))
                {
                    return (GeckoHtmlElement)node; 
                }
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private GeckoHtmlElement getGeckoHtmlElementHelp(GeckoDocument document)
        {
            GeckoElementCollection nodes = document.GetElementsByTagName("a");
            if (nodes == null) return null;
            foreach (GeckoElement node in nodes)
            {
                String href = ((GeckoHtmlElement)node).GetAttribute("href");
                if (href != null && (href.Contains("/help/212848065405122") || href.Contains("/help/174987089221178")))
                {
                    return (GeckoHtmlElement)node;
                }
            }
            return null;
        }
        /// <summary>
        /// <input type="hidden" value="1585885549" name="to" autocomplete="off">
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private GeckoHtmlElement getGeckoHtmlElementTo(GeckoDocument document)
        {
            GeckoHtmlElement to = document.GetElementsByName("to").FirstOrDefault();
            if (to.GetAttribute("type") == "hidden") return to;
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private GeckoHtmlElement getGeckoHtmlElementLogout(GeckoDocument document)
        {
            GeckoElementCollection nodes = document.GetElementsByTagName("a");
            if (nodes == null) return null;
            foreach (GeckoElement node in nodes)
            {
                String href = ((GeckoHtmlElement)node).GetAttribute("href");
                if (href != null && href.Contains("logout.php"))
                {
                    String logoutHtml = ((GeckoHtmlElement)node).InnerHtml;
                    if (logoutHtml.StartsWith("Logout") || logoutHtml.StartsWith("Đăng xuất") || logoutHtml.StartsWith("Log out") || logoutHtml.StartsWith("Log Out"))
                    {
                        return (GeckoHtmlElement)node;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private GeckoHtmlElement getGeckoHtmlElementNextWizard(GeckoDocument document)
        {
            //NEXT
            GeckoElementCollection nodes = document.GetElementsByTagName("a");
            if (nodes == null) return null;
            foreach (GeckoElement node in nodes)
            {
                if (node.TextContent == "Tiếp" || node.TextContent == "Next")
                {
                    return ((GeckoHtmlElement)node);
                }
            }
            //SAVE SETTINGS
            nodes = document.GetElementsByTagName("input");
            foreach (GeckoElement node in nodes)
            {
                String type = ((GeckoHtmlElement)node).GetAttribute("type");
                if (type != null && type.ToUpper() == "SUBMIT")
                {
                    String value = ((GeckoHtmlElement)node).GetAttribute("value");
                    if (value != null && (value.ToUpper() == "LƯU CÀI ĐẶT" || value.ToUpper() == "SAVE SETTINGS"))
                    {
                        return ((GeckoHtmlElement)node);
                    }
                    String name = ((GeckoHtmlElement)node).GetAttribute("name");
                    if (name != null && (name == "submit[Continue]"))
                    {
                        return ((GeckoHtmlElement)node);
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private GeckoHtmlElement getGeckoHtmlElementAcceptPlay(GeckoDocument document)
        {
            //logout.php
            GeckoElementCollection nodes = document.GetElementsByTagName("button");
            if (nodes == null) return null;
            foreach (GeckoElement node in nodes)
            {
                String name = ((GeckoHtmlElement)node).GetAttribute("name");
                if (name == "__CONFIRM__")
                {
                    return ((GeckoHtmlElement)node);
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private GeckoHtmlElement getGeckoHtmlElementSendConfirmCode(GeckoDocument document)
        {
            GeckoHtmlElement sendConfirmCode = (GeckoInputElement)document.GetElementsByClassName("w x y z ba").FirstOrDefault();
            if (sendConfirmCode != null && (sendConfirmCode.GetAttribute("value") == "Send Confirmation Code" || sendConfirmCode.GetAttribute("value") == "Gửi mã xác nhận"))
            {
                return sendConfirmCode;
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private GeckoHtmlElement getGeckoHtmlElementCode(GeckoDocument document)
        {
            GeckoElementCollection nodes = document.GetElementsByTagName("input");
            if (nodes == null) return null;
            foreach (GeckoElement node in nodes)
            {
                String name = ((GeckoHtmlElement)node).GetAttribute("name");
                if (name != null && name.Contains("code")) return (GeckoHtmlElement)node;
                String style = ((GeckoHtmlElement)node).GetAttribute("style");
                if (style != null && style.Contains("-wap-input-format")) return (GeckoHtmlElement)node;
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private GeckoHtmlElement getGeckoHtmlElementSubmit(GeckoDocument document)
        {
            GeckoElementCollection nodes = document.GetElementsByTagName("input");
            if (nodes == null) return null;
            foreach (GeckoElement node in nodes)
            {
                String type = ((GeckoHtmlElement)node).GetAttribute("type");
                if (type != null && type.ToUpper() == "SUBMIT")
                {
                    String value = ((GeckoHtmlElement)node).GetAttribute("value");
                    if (value != null && (value.ToUpper() == "CONTINUE" || value.ToUpper() == "SUBMIT" || value.ToUpper() == "CONFIRM" || value.ToUpper() == "CHẤP NHẬN" || value.ToUpper() == "GỬI" || value.ToUpper() == "TIẾP TỤC"))
                    {
                        return (GeckoHtmlElement)node;
                    }
                }
            }
            return null;
        }
        #endregion

        #region "DECAPTCHA"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private String getCaptchaCode(byte[] buffer)
        {
            int ret;

            uint p_pict_to;
            uint p_pict_type;
            uint major_id;
            uint minor_id;
            uint port;
            string host;
            string name;
            string passw;
            string answer_captcha;

            host = "xxxx";
            name = "xxx";
            passw = "xxxx";
            port = 36032;
            p_pict_to = 0;
            p_pict_type = 0;
            major_id = 0;
            minor_id = 0;

            ret = DecaptcherLib.Decaptcher.RecognizePicture(host, port, name, passw, buffer, out p_pict_to, out p_pict_type, out answer_captcha, out major_id, out minor_id);
            return answer_captcha;
        }

        #endregion

        #region "TIMER"
        private void timer_Reg_Tick(object sender, EventArgs e)
        {
            TIMER_REG.Enabled = false;
            TYPE = 1;
            STEP = 1;
            fillInfoFB();
        }

        private void timer_Accept_Tick(object sender, EventArgs e)
        {
            TIMER_ACCEPT.Enabled = false;
            geckoWebBrowser.Navigate(URL_ACCEPT_PK);
        }

        private void timer_Play_Tick(object sender, EventArgs e)
        {
            TIMER_PLAY.Enabled = false;
            TYPE = 2;
            STEP = 1;
            fillLoginFB();
        }

        private void timer_Invite_Tick(object sender, EventArgs e)
        {
            TIMER_INVITE.Enabled = false;
            TYPE = 3;
            STEP = 1;
            fillLoginInviteFB();
        }
        #endregion

        

        
    }
}
