var app = angular.module('D2DB', ['ui.bootstrap'])
app.filter('rawHtml', ['$sce', function ($sce) {  // i need this to display raw html 
    return function (val) {
        return $sce.trustAsHtml(val);
    };
}]);

app.filter('MyDate', ['$sce', function () {
    return function (val) {
        if (val == null || val == "")
            return ""
        ///Date(1080021600000)/

        var number = val.substr(val.indexOf("(") + 1, 13)
        return new Date(parseInt(number)).toLocaleDateString();
    };
}]);
function getDateShort(val) {
    if (val == null || val == "")
        return ""

    var number = val.substr(val.indexOf("(") + 1, 13)
    return new Date(parseInt(number)).toLocaleDateString();
}


app.controller('View', ['$scope', '$http', '$location', '$sce', function (scope, $http, $location) {
    scope.vm = window.vm;

    scope.ClassToName = ["Amazon", "Sorceress", "Necromancer", "Paladin", "Barbarian", "Druid", "Assassin"];
    scope.buildDiscription = function (item) {
        var ret = [];
        var color = ["n", "g", "s", "m", "u", "r", "r", "e", "c", "c"]
        item.description = item.description.replace(/(yc)\d/g ,"�c")
        var des = item.description.split("\n");
        var c = 0;
       
        c = des[0].indexOf("�c" > -1 ? 0 : des[0].substr(des[0].indexOf("�c"))+2, 1)
        for(var j = 0 ; j < des.length; j ++)
        {
            var l =""
            var line = des[j];
            var parts = line.split("�")
            for (var k = 0 ; k < parts.length; k++) {
                if (parts[k][0] == "c" && parts[k][1] < 10) {
                    c = parts[k][1];
                    parts[k]=parts[k].substr(2)
                }
                l = l + '<span class="' + color[parseInt(c)] + '">' + parts[k] + "</span>"                
            }
           ret=ret + l +"<nl><br>"
        }
        ret = ret.substr(0,ret.indexOf("</span>")+7) +"<br>"+ ret.substr(ret.indexOf("</span>") +7)
        ret = ret.split("<nl>").reverse().join("")
        ret = ret + '<br><span class="u">lvl :' + item.file + " " + item.color+'</span>';
        ret = ret.replace('<br><span class="undefined"></span><br>', "")
        ret = ret.replace('<br><br>', "")
        return ret
    }

    scope.deleteChar = function (car, account)
    {
        var host = "http://" + window.location.host + "/";
        $http.post(host + "Home/DeleteChar?id=" + car.Id).success(function (data, status, headers, config) {
            var i = account.Characters.indexOf(car)
            if(i != -1)
                account.Characters.splice(i, 1);
        }).error(function (data, status, headers, config) {
            var a = 1
        })
        ;
    }
    scope.viewChar= function(car)
    {
        window.location = "http://" + window.location.host + "/Home/ViewChar/"+car.Id;
    }

    scope.getItemPosition = function(item) {
        if (item.location == 3)
            return { x: item.x * 29, y: item.y * 29 }
        if(item.location ==1 )
        {
            if(item.bodylocation == 1)
                return { x:116, y:  -251 }
            if (item.bodylocation == 2)
                return { x: 188, y: -225 }
            if (item.bodylocation == 3)
                return { x: 116, y: -180 }
            if (item.bodylocation == 4)
                return { x: -1, y: -209 }
            if (item.bodylocation == 5)
                return { x: 231, y: -209 }
            if (item.bodylocation == 6)
                return { x: 74, y: -80 }
            if (item.bodylocation == 8)
                return { x: 117, y: -80 }
            if (item.bodylocation == 7)
                return { x: 188, y: -80 }
            if (item.bodylocation == 9)
                return { x: 234, y: -80 }
            if (item.bodylocation == 10)
                return { x: 2, y: -80 }
            if (item.bodylocation == 11)
                return { x: 65, y: -209 }
            if (item.bodylocation == 12)
                return { x: 300, y: -209 }
        }
    }
    scope.getDateDiff= function(t1, t)  {
        var number = t.substr(t.indexOf("(") + 1, 13)
        var t2 = new Date(parseInt(number));

        
        var diff = t1.getTime() - t2.getTime();

        var days = Math.floor(diff / (1000 * 60 * 60 * 24));
        diff -= days * (1000 * 60 * 60 * 24);

        var hours = Math.floor(diff / (1000 * 60 * 60));
        diff -= hours * (1000 * 60 * 60);

        var mins = Math.floor(diff / (1000 * 60));
        diff -= mins * (1000 * 60);

        var seconds = Math.floor(diff / (1000));
        diff -= seconds * (1000);

        
        return (days + ":" + ('0' + hours).slice(-2) + ':' + ('0' + mins).slice(-2) + ':' + ('0' + seconds).slice(-2));
    }
}]);