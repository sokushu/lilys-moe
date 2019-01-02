
/**
 * 封装了Ajax常用的方法
 * */
class Ajax {

    /**
     * 
     * @param _Url 发送的网址
     */
    public Ajax(Method: string, _Url: string, Data: { [key: string]: string; }, metho: Function) {

        var userInfo = "";
        for (let k in Data) {
            var value = Data[k];
            userInfo += k + "=" + value + "\n";
        }
        var HttpXmlReq = new XMLHttpRequest();
        //true是打开异步操作
        HttpXmlReq.open(Method, _Url, true);
        HttpXmlReq.withCredentials = true;
        HttpXmlReq.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        HttpXmlReq.onreadystatechange = function () {
            if (HttpXmlReq.readyState == 4) {
                if (HttpXmlReq.status == 200 || HttpXmlReq.status == 304) {
                    //返回一个[key value]格式的字符串
                    metho(HttpXmlReq.response);
                }
            }
        };
        HttpXmlReq.onerror = function () {
            alert("ERROR");
        };

        HttpXmlReq.send(userInfo);
    }
}