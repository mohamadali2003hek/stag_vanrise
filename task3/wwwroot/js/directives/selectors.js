angular.module("selectors", [])

    .directive("clientTypeSelector", function () {
        return {
            restrict: "E",
            scope: { selectedType: "=", getData: "=" },
            template: function (el, attrs) {
                return '<select class="form-control" ng-model="selectedType" ng-options="t.id as t.name for t in types">' +
                    (attrs.showAll === "true" ? '<option value="">All Types</option>' : '') + '</select>';
            },
            link: function (scope) {
                scope.types = [{ id: 0, name: "Individual" }, { id: 1, name: "Organization" }];
                scope.getData = function () { return scope.selectedType; };
            }
        };
    })

    .directive("deviceSelector", function ($http) {
        return {
            restrict: "E",
            scope: { selectedId: "=", getData: "=" },
            template: function (el, attrs) {
                return '<select class="form-control" ng-model="selectedId" ng-options="d.id as d.name for d in items">' +
                    (attrs.showAll === "true" ? '<option value="">All Devices</option>' : '') + '</select>';
            },
            link: function (scope) {
                scope.items = [];
                $http.get("/api/devices").then(function (r) { scope.items = r.data; });
                scope.getData = function () { return scope.selectedId; };
            }
        };
    })

    .directive("clientSelector", function ($http) {
        return {
            restrict: "E",
            scope: { selectedId: "=", getData: "=" },
            template: function (el, attrs) {
                return '<select class="form-control" ng-model="selectedId" ng-options="c.id as c.name for c in items">' +
                    (attrs.showAll === "true" ? '<option value="">All Clients</option>' : '') + '</select>';
            },
            link: function (scope) {
                scope.items = [];
                $http.get("/api/ClientsApi").then(function (r) { scope.items = r.data; });
                scope.getData = function () { return scope.selectedId; };
            }
        };
    })

    .directive("phoneNumberSelector", function ($http) {
        return {
            restrict: "E",
            scope: { selectedId: "=", getData: "=" },
            template: function (el, attrs) {
                return '<select class="form-control" ng-model="selectedId" ng-options="p.id as p.number for p in items">' +
                    (attrs.showAll === "true" ? '<option value="">All Numbers</option>' : '') + '</select>';
            },
            link: function (scope) {
                scope.items = [];
                $http.get("/api/PhoneNumbersApi").then(function (r) { scope.items = r.data; });
                scope.getData = function () { return scope.selectedId; };
            }
        };
    });