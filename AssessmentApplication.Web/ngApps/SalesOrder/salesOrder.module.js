(function () {
	"use strict";

	angular
		.module("salesOrderModule", ["ngRoute", "ngSanitize", "ui.bootstrap", config])
		.factory("authHttpResponseInterceptor", ["$q", "$window", services]);

	function config($httpProvider, $locationProvider, $routeProvider) {
		$routeProvider
			.when("/detail/:salesOrderId", {
				controller: "salesOrderDetailController",
				controllerAs: "vm",
				templateUrl: "/ngApps/SalesOrder/Views/Detail.html"
			})
			.when("/search", {
				controller: "salesOrderSearchController",
				controllerAs: "vm",
				templateUrl: "/ngApps/SalesOrder/Views/Search.html"
			})
			.otherwise({
				redirectTo: "/search"
			});

		// Http Intercpetor to check auth failures for xhr requests
		$httpProvider.interceptors.push("authHttpResponseInterceptor");

		$locationProvider.html5Mode(true).hashPrefix("!");
	}

	function services($q, $window) {
		return {
			response: function(response) {
				// do something on success
				return response || $q.when(response);
			},
			responseError: function(rejection) {
				// do something on error
				if (rejection.status === 401) {
					$window.location = "/Account/Login";
				}

				return $q.reject(rejection);
			}
		};
	}
})();