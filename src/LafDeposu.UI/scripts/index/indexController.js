var findWordsApp = angular.module("findWordsApp", ["ngResource", "ngCookies"]);

findWordsApp.factory("FindWord", ["$resource", function ($resource) {
    return {
        database: $resource("FindWord/:chars?startsWith=:startsWith&contains=:contains&endsWith=:endsWith&resultCharCount=:resultCharCount")
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

findWordsApp.controller("wordListCtrl", ["$scope", "FindWord", "Share", "$cookies", function ($scope, FindWord, Share, $cookies) {
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

            $scope.share = Share.createLink($scope.chars, $scope.startsWith, $scope.contains, $scope.endsWith, $scope.resultCharCount);
        });

        // Kullanıcının tercihini kaydedelim
        $scope.changeListType = function (value) {
            $cookies.listType = value;
            $scope.listType = value;
        }
    }
}]);