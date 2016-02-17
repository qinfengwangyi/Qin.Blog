/*
$.cookie(‘the_cookie’); // 读取 cookie
$.cookie(‘the_cookie’, 'the_value’); // 存储 cookie
$.cookie(‘the_cookie’, 'the_value’, { expires: 7 }); // 存储一个带7天期限的 cookie
$.cookie(‘the_cookie’, '', { expires: -1 }); // 删除 cookie 
*/
jQuery.extend({
    format: function() {
        if (arguments.length == 0)
            return null;

        var str = arguments[0];
        for (var i = 1; i < arguments.length; i++) {
            var re = new RegExp('\\{' + (i - 1) + '\\}', 'gm');
            str = str.replace(re, arguments[i]);
        }
        return str;
    },
    serialize: function(obj) {
        var ransferCharForJavascript = function(s) {
            var newStr = s.replace(
                            /[\x26\x27\x3C\x3E\x0D\x0A\x22\x2C\x5C\x00]/g,
                            function(c) {
                                ascii = c.charCodeAt(0)
                                return '\\u00' + (ascii < 16 ? '0' + ascii.toString(16) : ascii.toString(16))
                            }
                        );
            return newStr;
        }

        if (obj == null) {
            return null
        }

        else if (obj.constructor == Array) {
            var builder = [];
            builder.push("[");
            for (var index in obj) {
                if (typeof obj[index] == "function") continue;
                if (index > 0) builder.push(",");
                builder.push($.serialize(obj[index]));
            }
            builder.push("]");
            return builder.join("");
        }

        else if (obj.constructor == Object) {
            var builder = [];
            builder.push("{");
            var index = 0;
            for (var key in obj) {
                if (typeof obj[key] == "function") continue;
                if (index > 0) {
                    builder.push(",");
                }
                builder.push($.format("\"{0}\":{1}", key, $.serialize(obj[key])));
                index++;
            }
            builder.push("}");
            return builder.join("");
        }

        else if (obj.constructor == Boolean) {
            return obj.toString();
        }

        else if (obj.constructor == Number) {
            return obj.toString();
        }

        else if (obj.constructor == String) {
            return $.format('"{0}"', ransferCharForJavascript(obj));
        }

        else if (obj.constructor == Date) {
            return $.format('{"__DataType":"Date","__thisue":{0}}', obj.getTime() - (new Date(1970, 0, 1, 0, 0, 0)).getTime());
        }

        else if (this.toString != undefined) {
            return $.serialize(obj);
        }
    },
    cookie: function(name, value, options) {
        if (typeof value != 'undefined') {	// name and value given, set cookie
            options = options || {};
            if (value === null) {	// Delete a cookie by passing null as value
                value = '';
                options.expires = -1;
            }
            var expires = '';
            if (options.expires && (typeof options.expires == 'number' || options.expires.toUTCString)) {
                var date;
                if (typeof options.expires == 'number') {	// 几天内有效
                    date = new Date();
                    date.setTime(date.getTime() + (options.expires * 24 * 60 * 60 * 1000));
                }
                else {	// 此前有效
                    date = options.expires;
                }
                expires = '; expires=' + date.toUTCString(); // use expires attribute, max-age is not supported by IE
            }
            var path = options.path ? '; path=' + (options.path) : '';
            var domain = options.domain ? '; domain=' + (options.domain) : '';
            var secure = options.secure ? '; secure' : '';
            document.cookie = [name, '=', encodeURIComponent(value), expires, path, domain, secure].join('');
        }
        else {	// only name given, get cookie
            var cookieValue = null;
            if (document.cookie && document.cookie != '') {
                var cookies = document.cookie.split(';');
                for (var i = 0; i < cookies.length; i++) {
                    var cookie = jQuery.trim(cookies[i]);
                    // Does this cookie string begin with the name we want?
                    if (cookie.substring(0, name.length + 1) == (name + '=')) {
                        cookieValue = decodeURIComponent(cookie.substring(name.length + 1));
                        break;
                    }
                }
            }
            return cookieValue;
        }
    },
    query: {
        get: function(key) {
            var reg = new RegExp("\\?(?:.+&)?" + key + "=(.*?)(?:&.*)?$");
            var mch = window.location.href.match(reg);
            return mch ? mch[1].replace('#', '') : "";
        }
    },
    // 时间对象
    date: {
        characters: ['〇', '一', '二', '三', '四', '五', '六', '七', '八', '九'],
        toUpperCase: function(value, literal) {
            if (literal) {
                return (String(value)).replace(/([0-9])/g, function($0) {//[0-9])(\1*)
                    return $.date.characters[$0];
                });
                /*var result = [];
                value = "" + value;
                for (var i = 0; i < value.length; i++) {
                result.push($.date.characters[parseInt(value.charAt(i), 10)]);
                }
                return result.join('');*/
            }
            else {
                value = Number(value);
                if (value < 10) {
                    return $.date.characters[value];
                }
                else if (value < 20) {
                    return '十' + (value % 10 == 0 ? '' : $.date.characters[value % 10]);
                }
                else if (value < 100) {
                    return $.date.characters[Math.floor(value / 10)] + '十' + (value % 10 == 0 ? '' : $.date.characters[value % 10]);
                }
                else if (value < 1000) {
                    return $.date.characters[Math.floor(value / 100)] + '百' + (value % 100 >= 10 ? $.date.characters[Math.floor((value % 100) / 10)] + '十' : (value % 10 > 0 ? '零' : '')) + (value % 10 > 0 ? $.date.characters[value % 10] : '');
                }
                else {
                    return value;
                }
            }
        },
        format: function(date, pattern, upperCase) {
            // 
            var zeroize = function(value, length) {
                if (!length) {
                    length = 2;
                }
                value = String(value);
                for (var i = 0, zeros = ''; i < (length - value.length); i++) {
                    zeros += '0';
                }
                return zeros + value;
            };
            // 
            return pattern.replace(/"[^"]*"|'[^']*'|\b(?:d{1,4}|m{1,4}|yy(?:yy)?|([hHMstT])\1?|[lLZ])\b/g, function($0) {
                switch ($0) {
                    case 'd':
                        return !upperCase ? date.getDate() : $.date.toUpperCase(date.getDate());
                    case 'dd':
                        return !upperCase ? zeroize(date.getDate()) : $.date.toUpperCase(date.getDate());
                    case 'ddd':
                        return ['Sun', 'Mon', 'Tue', 'Wed', 'Thr', 'Fri', 'Sat'][date.getDay()];
                    case 'dddd':
                        return ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'][date.getDay()];
                    case 'M':
                        return !upperCase ? (date.getMonth() + 1) : $.date.toUpperCase(date.getMonth() + 1);
                    case 'MM':
                        return !upperCase ? zeroize(date.getMonth() + 1) : $.date.toUpperCase(date.getMonth() + 1);
                    case 'MMM':
                        return ['一', '二', '三', '四', '五', '六', '七', '八', '九', '十', '十一', '十二'][date.getMonth()];
                    case 'MMMM':
                        return ['一', '二', '三', '四', '五', '六', '七', '八', '九', '十', '十一', '十二'][date.getMonth()];
                    case 'yy':
                        return !upperCase ? String(date.getFullYear()).substr(2) : $.date.toUpperCase(String(date.getFullYear()).substr(2), true);
                    case 'yyyy':
                        return !upperCase ? date.getFullYear() : $.date.toUpperCase(date.getFullYear(), true);
                    case 'h':
                        return !upperCase ? (date.getHours() % 12 || 12) : $.date.toUpperCase(date.getHours() % 12 || 12);
                    case 'hh':
                        return !upperCase ? zeroize(date.getHours() % 12 || 12) : $.date.toUpperCase(date.getHours() % 12 || 12);
                    case 'H':
                        return !upperCase ? date.getHours() : $.date.toUpperCase(date.getHours());
                    case 'HH':
                        return !upperCase ? zeroize(date.getHours()) : $.date.toUpperCase(date.getHours());
                    case 'm':
                        return !upperCase ? date.getMinutes() : $.date.toUpperCase(date.getMinutes());
                    case 'mm':
                        return !upperCase ? zeroize(date.getMinutes()) : $.date.toUpperCase(date.getMinutes());
                    case 's':
                        return !upperCase ? date.getSeconds() : $.date.toUpperCase(date.getSeconds());
                    case 'ss':
                        return !upperCase ? zeroize(date.getSeconds()) : $.date.toUpperCase(date.getSeconds());
                    case 'l':
                        return !upperCase ? zeroize(date.getMilliseconds(), 3) : $.date.toUpperCase(date.getMilliseconds());  //, date.getMilliseconds() > 100
                    case 'L':
                        var m = date.getMilliseconds();
                        if (m > 99) {
                            m = Math.round(m / 10);
                        }
                        return zeroize(m);
                    case 'tt':
                        return date.getHours() < 12 ? '上午' : '下午';
                    case 'TT':
                        return date.getHours() < 12 ? '上午' : '下午';
                    case 'Z':
                        return date.toUTCString().match(/[A-Z]+$/);
                    default:
                        // Return quoted strings with the surrounding quotes removed  
                        return $0.substr(1, $0.length - 2);

                }
            });
        },
        parse: function(value, pattern) {
            var matchs1 = pattern.match(/"[^"]*"|'[^']*'|\b(?:d{1,4}|m{1,4}|yy(?:yy)?|([hHMs])\1?|[l])\b/g);
            var matchs2 = value.match(/(\d+)/g);

            if (matchs1.length == matchs2.length) {
                var date = new Date(1970, 0, 1);
                for (var i = 0; i < matchs1.length; i++) {
                    var field = parseInt(matchs2[i], 10);
                    var sign = matchs1[i];
                    switch (sign.charAt(0)) {
                        case 'y': date.setFullYear(field); break;
                        case 'M': date.setMonth(field - 1); break;
                        case 'd': date.setDate(field); break;
                        case 'H': date.setHours(field); break;
                        case 'm': date.setMinutes(field); break;
                        case 's': date.setSeconds(field); break;
                        case 'l': date.setMilliseconds(field); break;
                    }
                }
                return date;
            }

            return null;
        }
    },
    modifiers: {
        formatDate: function(date, pattern, upperCase) {
            //return $.date.format(new Date(Number(date.replace(/\/Date\((\d+)\)\//, "$1"))), pattern, upperCase);
            var regExp = new RegExp("(-?\\d+)"); //[0-9]
            var matchs = date.match(regExp);
            if (matchs) {
                return $.date.format(new Date(Number(matchs[1])), pattern || 'yyyy-MM-dd HH:mm:ss', upperCase);
            }
            return "";
        }
    },
    HTMLEncode:function(content){
        var temp = document.createElement("div"); 
        (temp.textContent != null) ? (temp.textContent = content) : (temp.innerText = content); 
        var output = temp.innerHTML; 
        temp = null; 
        return output;
    },
    HTMLDecode:function(content){
        var temp = document.createElement("div"); 
        temp.innerHTML = content; 
        var output = temp.innerText || temp.textContent; 
        temp = null; 
        return output; 
    },
    getUplaodPath: function(obj){
        var path=$(obj).val();
        if(obj){
            if (window.navigator.userAgent.indexOf("MSIE")>=1){
                obj.select();
                path = document.selection.createRange().text;
            }
            else if(window.navigator.userAgent.indexOf("Firefox")>=1){
                if(obj.files){
                    path = obj.files.item(0).getAsDataURL();
                }else
                {
                    path = obj.value
                }
            }else
            {
                path = obj.value;
            }
        }
        return path;
    },
    ExtConvert: {
        ToCharArray :function(str){
            var str_arr = new Array();
            len = str.length;
            for (i=0;i<len;i++) {
                str_arr[i] = str.substring(i, i+1);
            }
            return str_arr;
        },
        ToChinese: function(str) {
             //"零壹贰叁肆伍陆柒捌玖拾佰仟萬億圆整角分"
            var cstr = "";
            var chararray = $.ExtConvert.ToCharArray(str);
            for (var i = 0; i < chararray.length; i++) {
                switch (chararray[i]) {
                    case '0': cstr += ""; break;
                    case '1': cstr += "一"; break;
                    case '2': cstr += "二"; break;
                    case '3': cstr += "三"; break;
                    case '4': cstr += "四"; break;
                    case '5': cstr += "五"; break;
                    case '6': cstr += "六"; break;
                    case '7': cstr += "七"; break;
                    case '8': cstr += "八"; break;
                    case '9': cstr += "九"; break;
                    default: cstr +=chararray[i];break;
                }
                switch (chararray.length - i) {
                    case '0': cstr += ""; break;
                    case '1': cstr += ""; break;
                    case '2': cstr += "十"; break;
                    case '3': cstr += "百"; break;
                    case '4': cstr += "千"; break;
                    case '5': cstr += "万"; break;
                    case '6': cstr += "十"; break;
                    case '7': cstr += "百"; break;
                    case '8': cstr += "千"; break;
                    case '9': cstr += "亿"; break;
                }
            }
            return (cstr);
        }
    }
});

