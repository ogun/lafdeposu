findWordsApp.factory("FindWord", ["$resource", function ($resource) {
    return {
        database: $resource("Kelime/Getir/:chars?startsWith=:startsWith&contains=:contains&endsWith=:endsWith&resultCharCount=:resultCharCount")
    }
}]);

findWordsApp.factory("Share", function () {
    var createLink = function (chars, startsWith, contains, endsWith, resultCharCount) {
        var returnValue = {};
        returnValue.queryString = "";

        if (chars != null && chars != "") {
            returnValue.queryString = "keyword=" + chars;

            if (startsWith != null && startsWith != "") {
                returnValue.queryString += "&startsWith=" + startsWith;
            }

            if (contains != null && contains != "") {
                returnValue.queryString += "&contains=" + contains;
            }

            if (endsWith != null && endsWith != "") {
                returnValue.queryString += "&endsWith=" + endsWith;
            }

            if (resultCharCount != null && resultCharCount != "") {
                returnValue.queryString += "&resultCharCount=2";
            }
        }

        return returnValue;
    }

    return {
        createLink: createLink
    }
});

findWordsApp.controller("wordListCtrl", ["$scope", "$sce", "FindWord", "Share", "$cookies", function ($scope, $sce, FindWord, Share, $cookies) {
    $scope.listType = $cookies.listType;

    $scope.findWordsClick = function () {
        $loadingContainer = $("#loadingContainer");
        $loadingContainer.removeClass("hide");

        $scope.wordList = FindWord.database.query({
            chars: $scope.chars,
            startsWith: $scope.startsWith,
            contains: $scope.contains,
            endsWith: $scope.endsWith,
            resultCharCount: $scope.resultCharCount ? 2 : null
        });

        $scope.wordList.$promise["finally"](function () {
            $loadingContainer.addClass("hide");

            $scope.wordList.maxRowCount = function () {
                var maxRow = Math.max.apply(Math, $.map($scope.wordList, function (el) { return el.words.length; }));
                var arr = new Array(maxRow);
                for (var i = 0; i < maxRow; i++) {
                    arr[i] = i;
                }
                return arr;
            }
            
            for (var i = 0; i < $scope.wordList.length; i++) {
                var wordGroup = $scope.wordList[i];

                for (var j = 0; j < wordGroup.words.length; j++) {
                    var word = wordGroup.words[j].w;
                    var jokers = wordGroup.words[j].j.split("");

                    for (var k = 0; k < jokers.length; k++) {
                        var joker = jokers[k];

                        var regex = new RegExp(joker + "(?!<)", "");
                        word = word.replace(regex, "<span class=\"j\">" + joker + "</span>");
                    }

                    $scope.wordList[i].words[j].w = $sce.trustAsHtml(word);
                }
            }

            $scope.share = Share.createLink($scope.chars, $scope.startsWith, $scope.contains, $scope.endsWith, $scope.resultCharCount);
        });
    }



    // Kullanıcının tercihini kaydedelim
    $scope.changeListType = function (value) {
        $cookies.listType = value;
        $scope.listType = value;
    }
}]);