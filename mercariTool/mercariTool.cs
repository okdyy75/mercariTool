using System;
using System.Net;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using Codeplex.Data;

namespace mercariTool
{
    public partial class MainForm : Form
    {
        // 定数設定
        private String MERCARI_URL = "https://www.mercari.com";
        private String MYPAGE_URL = "https://www.mercari.com/jp/mypage/";
        private String LOGIN_URL = "https://www.mercari.com/jp/login/";
        private String SELL_URL = "https://www.mercari.com/jp/sell/";
        private String INI_FILENAME = "init.txt";
        private String PHPSESSID;

        private int COLUMN_INDEX_ITEMID = 0;
        private int COLUMN_INDEX_STATUS = 1;
        private int COLUMN_INDEX_ITEMNAME = 2;
        private int COLUMN_INDEX_IINE = 3;
        private int COLUMN_INDEX_COMMENT = 4;
        private int COLUMN_INDEX_SELL = 5;
        private int COLUMN_INDEX_SELECTDEL = 6;

        private CookieContainer myCookie;
        private bool isLogin;

        public MainForm()
        {
            InitializeComponent();
            myCookie = new CookieContainer();
            isLogin = false;
        }

        // [PHPSESSIDログイン]ボタンクリック
        private void PHPSESSID_Click(object sender, EventArgs e)
        {
            var ini = new IniFile(INI_FILENAME);

            if (!System.IO.File.Exists(ini.FullName))
            {
                MessageBox.Show("exeと同じフォルダに「" + INI_FILENAME + "」ファイルを設置して下さい");
                return;
            }
            PHPSESSID = ini.Read("global", "PHPSESSID");

            myCookie.Add(new Uri(MERCARI_URL), (new Cookie("PHPSESSID", PHPSESSID)));
            loginCheck();
        }

        // [出品商品一覧取得]ボタンクリック
        private void getItemList_button_click(object sender, EventArgs e)
        {
            if (!isLogin)
            {
                MessageBox.Show("ログインしてください");
                return;
            }

            getItemList();
        }


        // [全部再出品]ボタンクリック
        private void allSell_button_Click(object sender, EventArgs e)
        {
            if (!isLogin)
            {
                MessageBox.Show("ログインしてください");
                return;
            }

            if (itemList_dataGridView.Rows.Count == 0)
            {
                MessageBox.Show("出品データがありません");
                return;
            }

            foreach (DataGridViewRow row in itemList_dataGridView.Rows)
            {
                long itemId = long.Parse(row.Cells[COLUMN_INDEX_ITEMID].Value.ToString());
                String status = row.Cells[COLUMN_INDEX_STATUS].Value.ToString();
                if (status == "出品中")
                {
                    sellItem(itemId);
                }
            }
            MessageBox.Show("出品中の商品をすべて再出品しました");
        }

        // [選択削除]ボタンクリック
        private void checkDelete_button_Click(object sender, EventArgs e)
        {
            if (!isLogin)
            {
                MessageBox.Show("ログインしてください");
                return;
            }

            if (itemList_dataGridView.Rows.Count == 0)
            {
                MessageBox.Show("出品データがありません");
                return;
            }

            DialogResult result = MessageBox.Show("本当によろしいですか？", "削除", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (result == DialogResult.OK)
            {
                foreach (DataGridViewRow row in itemList_dataGridView.Rows)
                {
                    long itemId = long.Parse(row.Cells[COLUMN_INDEX_ITEMID].Value.ToString());
                    bool chk = (bool)(row.Cells[COLUMN_INDEX_SELECTDEL].Value ?? false);
                    if (chk == true)
                    {
                        deleteItem(itemId);
                    }
                }
                getItemList();
                MessageBox.Show("選択した商品をすべて削除しました");
            }
        }

        // データ表クリック
        private void itemList_dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!isLogin)
            {
                return;
            }

            if (itemList_dataGridView.Rows.Count == 0)
            {
                return;
            }

            int colIdx = e.ColumnIndex;
            int rowIdx = e.RowIndex;

            // [出品]ボタンクリック
            if (colIdx == COLUMN_INDEX_SELL)
            {
                long itemId = long.Parse(itemList_dataGridView[0, rowIdx].Value.ToString());
                if (sellItem(itemId))
                {
                    MessageBox.Show("出品が完了しました");
                } else
                {
                    MessageBox.Show("出品に失敗しました");
                }
            }

            // [選択削除]ヘッダークリック
            if (rowIdx == -1 && colIdx == COLUMN_INDEX_SELECTDEL)
            {
                DataGridViewRow row1 = itemList_dataGridView.Rows[0];
                bool chk = (bool)(row1.Cells[COLUMN_INDEX_SELECTDEL].Value ?? false);
                foreach (DataGridViewRow row in itemList_dataGridView.Rows)
                {
                    row.Cells[COLUMN_INDEX_SELECTDEL].Value = !chk;
                }
            }
        }

        //出品商品一覧取得処理
        private void getItemList()
        {
            try
            {
                String html = webRequest("get", "https://www.mercari.com/jp/mypage/listings/listing/");

                //スクレイピング
                HtmlAgilityPack.HtmlDocument agility_html_doc = new HtmlAgilityPack.HtmlDocument();
                agility_html_doc.LoadHtml(html);

                HtmlAgilityPack.HtmlNodeCollection list = agility_html_doc.DocumentNode.SelectNodes("//ul[@id='mypage-tab-transaction-now']//li");

                itemList_dataGridView.Rows.Clear();
                foreach (HtmlAgilityPack.HtmlNode item in list)
                {
                    long itemId = 0;
                    String status = "";
                    String itemName = "";
                    int IIne = 0;
                    int comment = 0;

                    HtmlAgilityPack.HtmlDocument item_doc = new HtmlAgilityPack.HtmlDocument();
                    item_doc.LoadHtml(item.InnerHtml);

                    String link = item_doc.DocumentNode.SelectNodes("//a[@class='mypage-item-link']")[0].Attributes["href"].Value;
                    Match m = Regex.Matches(link, @"m(\d+)")[0];
                    itemId = long.Parse(m.Groups[1].Value);
                    status = item_doc.DocumentNode.SelectNodes("//div[contains(@class, 'mypage-item-status')]")[0].InnerText.Trim();
                    itemName = item_doc.DocumentNode.SelectNodes("//div[@class='mypage-item-text']")[0].InnerText.Trim();
                    IIne = int.Parse(item_doc.DocumentNode.SelectNodes("//span[@class='listing-item-count']")[0].InnerText);
                    comment = int.Parse(item_doc.DocumentNode.SelectNodes("//span[@class='listing-item-count']")[1].InnerText);

                    itemList_dataGridView.Rows.Add(new Object[] { itemId, status, itemName, IIne, comment });

                }
                itemCont_label.Text = itemList_dataGridView.Rows.Count.ToString() + "件";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        // 出品処理
        private bool sellItem(long itemId)
        {
            try
            {
                //WebRequestの作成
                String html = webRequest("get", SELL_URL);

                //スクレイピング
                html = html.Replace("\r", "").Replace("\n", "");
                MatchCollection matche1 = Regex.Matches(html, "render(\\(.*?\\))");
                MatchCollection matche2 = Regex.Matches(matche1[0].Value, "\\{.*?\\}, '(.+)', '(.+)'");
                String __csrf_value = matche2[0].Groups[1].Value;
                String exhibit_token = matche2[0].Groups[2].Value;

                //WebRequestの作成
                html = webRequest("get", "https://www.mercari.com/jp/sell/item_json/m" + itemId + "/");

                dynamic json = DynamicJson.Parse(html);

                String name = json.item.name;
                String description = json.item.description;
                String root_category_id = json.item.root_category_id;
                String parent_category_id = json.item.parent_category_id;
                String category_id = json.item.category_id;
                String size = json.item.size;
                String size_group_id = json.item.size_group_id;
                String brand_name = json.item.brand_name;
                String brand_name_label = json.item.brand_name_label;
                String brand_group_id = json.item.brand_group_id;
                String item_condition = json.item.item_condition;
                String shipping_payer = json.item.shipping_payer;
                String shipping_method = json.item.shipping_method;
                String shipping_from_area = json.item.shipping_from_area;
                String shipping_duration = json.item.shipping_duration;
                int price = int.Parse(json.item.price);
                int sales_fee = (price / 10);

                var imgLength = ((object[])json.item.photo_paths).Length;
                String image1 = (imgLength >= 1) ? json.item.photo_paths[0] : "";
                String image2 = (imgLength >= 2) ? json.item.photo_paths[1] : "";
                String image3 = (imgLength >= 3) ? json.item.photo_paths[2] : "";
                String image4 = (imgLength >= 4) ? json.item.photo_paths[3] : "";
                String image5 = (imgLength >= 5) ? json.item.photo_paths[4] : "";
                String image6 = (imgLength >= 6) ? json.item.photo_paths[5] : "";
                String image7 = (imgLength >= 7) ? json.item.photo_paths[6] : "";
                String image8 = (imgLength >= 8) ? json.item.photo_paths[7] : "";
                String image9 = (imgLength >= 9) ? json.item.photo_paths[8] : "";
                String image10 = (imgLength >= 10) ? json.item.photo_paths[9] : "";

                //if (price >= 10000)
                //{
                //    sales_fee = (int)Math.Floor(price * 0.05) + 500;
                //}


                //POST送信する文字列を作成
                string postData =
                   "__csrf_value=" + System.Web.HttpUtility.UrlEncode(__csrf_value)
                   + "&exhibit_token=" + System.Web.HttpUtility.UrlEncode(exhibit_token)
                   + "&brand_name=" + brand_name
                   + "&category_id=" + category_id
                   + "&description=" + System.Web.HttpUtility.UrlEncode(description)
                   + "&image1=" + System.Web.HttpUtility.UrlEncode(image1)
                   + "&image2=" + System.Web.HttpUtility.UrlEncode(image2)
                   + "&image3=" + System.Web.HttpUtility.UrlEncode(image3)
                   + "&image4=" + System.Web.HttpUtility.UrlEncode(image4)
                   + "&image5=" + System.Web.HttpUtility.UrlEncode(image5)
                   + "&image6=" + System.Web.HttpUtility.UrlEncode(image6)
                   + "&image7=" + System.Web.HttpUtility.UrlEncode(image7)
                   + "&image8=" + System.Web.HttpUtility.UrlEncode(image8)
                   + "&image9=" + System.Web.HttpUtility.UrlEncode(image9)
                   + "&image10=" + System.Web.HttpUtility.UrlEncode(image10)
                   + "&item_condition=" + item_condition
                   + "&name=" + System.Web.HttpUtility.UrlEncode(name)
                   + "&price=" + price
                   + "&sales_fee=" + sales_fee
                   + "&shipping_duration=" + shipping_duration
                   + "&shipping_from_area=" + shipping_from_area
                   + "&shipping_method=" + shipping_method
                   + "&shipping_payer=" + shipping_payer
                   + "&size=" + size
                   ;

                //WebRequestの作成
                webRequest("post", "https://www.mercari.com/jp/sell/selling/", postData);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

        }

        // 出品削除処理
        private void deleteItem(long itemId)
        {

            //WebRequestの作成
            String html = webRequest("get", "https://www.mercari.com/jp/items/m" + itemId + "/ ");

            //スクレイピング
            HtmlAgilityPack.HtmlDocument agility_html_doc = new HtmlAgilityPack.HtmlDocument();
            agility_html_doc.LoadHtml(html);
            String __csrf_value = agility_html_doc.DocumentNode.SelectNodes("//input[@name='__csrf_value']")[0].Attributes["value"].Value;

            //POST送信する文字列を作成
            string postData = "__csrf_value=" + System.Web.HttpUtility.UrlEncode(__csrf_value);

            //WebRequestの作成
            html = webRequest("post", "https://www.mercari.com/jp/items/cancel/m" + itemId + "/ ", postData);

        }

        // ログインチェック
        private void loginCheck()
        {
            try
            {
                isLogin = false;

                //WebRequestの作成
                String html = webRequest("get", MYPAGE_URL);

                HtmlAgilityPack.HtmlDocument agility_html_doc = new HtmlAgilityPack.HtmlDocument();
                agility_html_doc.LoadHtml(html);
                String canonical = (agility_html_doc.DocumentNode.SelectNodes("//link[@rel='canonical']") != null) ? agility_html_doc.DocumentNode.SelectNodes("//link[@rel='canonical']")[0].Attributes["href"].Value : "";

                if (canonical == MYPAGE_URL)
                {
                    login_label.BackColor = System.Drawing.Color.Blue;
                    isLogin = true;
                }
                else
                {
                    login_label.BackColor = System.Drawing.Color.Orange;
                    MessageBox.Show("ログインに失敗しました");
                    isLogin = false;
                }

            }
            catch (WebException e)
            {
                Console.WriteLine(e);
            }
        }

        // URLリクエスト
        private String webRequest(String method, String url, String postData = "")
        {
            String body = "";
            try
            {

                if (method == "get")
                {
                    //WebRequestの作成
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.CookieContainer = myCookie;
                    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream stream = response.GetResponseStream();
                    StreamReader streader = new StreamReader(stream, System.Text.Encoding.GetEncoding("UTF-8"));
                    body = streader.ReadToEnd();

                    stream.Close();
                    streader.Close();
                }
                else if (method == "post")
                {
                    //バイト型配列に変換
                    byte[] postDataBytes = System.Text.Encoding.ASCII.GetBytes(postData);

                    //WebRequestの作成
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.CookieContainer = myCookie;
                    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = postDataBytes.Length;

                    Stream stream = request.GetRequestStream();

                    //送信するデータを書き込む
                    stream.Write(postDataBytes, 0, postDataBytes.Length);

                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    stream = response.GetResponseStream();
                    StreamReader streader = new System.IO.StreamReader(stream, System.Text.Encoding.GetEncoding("UTF-8"));

                    body = streader.ReadToEnd();

                    stream.Close();
                    streader.Close();
                }

            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("err");
                WebResponse response = ex.Response;
                Stream data = response.GetResponseStream();
                StreamReader sr = new StreamReader(data);
                dynamic json = DynamicJson.Parse(sr.ReadToEnd());
                Console.WriteLine(json);
                throw ex;
            }

            return body;
        }
    }
}
