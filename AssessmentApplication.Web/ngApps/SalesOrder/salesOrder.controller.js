(function () {
	"use strict";

	angular
		.module("salesOrderModule")
		.controller("salesOrderDetailController", ["$routeParams", "$scope", "$timeout", "modalService", "salesOrderService", salesOrderDetailController])
		.controller("salesOrderSearchController", ["$scope", "$timeout", "modalService", "salesOrderService", salesOrderSearchController]);

	function salesOrderDetailController($routeParams, $scope, $timeout, modalService, salesOrderService) {
		var that = this;
		this.contentLoading = false;
		this.formModel = new DetailFormModel();
		this.salesOrderId = $routeParams.salesOrderId;

		this.dataLoaded = function () {
			return that.formModel && angular.isArray(that.formModel.details) && that.formModel.details.length > 0;
		};

		this.loadData = function (salesOrderId) {
			that.contentLoading = true;

			$timeout(function() {
				salesOrderService
					.getSalesOrderDetail(salesOrderId)
					.then(function(response) {
							that.formModel.details = response.data;
						},
						function(err) {
							alert("error: " + err.statusText);
						})
					.finally(function() {
						that.contentLoading = false;
					});
			});
		};

		$scope.$watch(function () {
				return that.contentLoading;
			},
			function (value) {
				if (value) {
					modalService.showModal();
				} else {
					modalService.hideModal();
				}
			});

		this.loadData(this.salesOrderId);
	}

	function salesOrderSearchController($scope, $timeout, modalService, salesOrderService) {
		var that = this;
		this.contentLoading = false;
		this.formModel = new SearchFormModel();
		this.getSalesOrderSearch = getSalesOrderSearch;
		this.showPopup = showPopup;

		this.dataLoaded = function() {
			return that.formModel && angular.isArray(that.formModel.salesOrders) && that.formModel.salesOrders.length > 0;
		};

		this.noDataLoaded = function() {
			return that.formModel.searchButtonClicked && !that.dataLoaded();
		};

		this.resetForm = function (formName) {
			that.contentLoading = true;

			$timeout(function() {
				var form = $scope[formName];

				that.formModel.resetValues();
				form.$setUntouched();
				form.$setPristine();

				that.contentLoading = false;
			});
		};

		function getSalesOrderSearch(event) {
			event.preventDefault();
			that.contentLoading = true;

			$timeout(function () {
				salesOrderService
					.getSalesOrderSearch(that.formModel.customerName, that.formModel.dueDate, that.formModel.orderDate, that.formModel.shipDate)
					.then(function (response) {
							that.formModel.salesOrders = response.data;
						},
						function (err) {
							alert("error: " + err.statusText);
						})
					.finally(function () {
						that.contentLoading = false;
						that.formModel.searchButtonClicked = true;
					});
			});
		}

		function showPopup(name) {
			var arr = [that.formModel.dueDate, that.formModel.orderDate, that.formModel.shipDate];

			angular.forEach(arr,
				function (value, key) {
					value.endDatePopupOpened = false;
					value.startDatePopupOpened = false;
				});

			switch (name) {
				case "orderDateStart":
					that.formModel.orderDate.startDatePopupOpened = true;
					break;

				case "orderDateEnd":
					that.formModel.orderDate.endDatePopupOpened = true;
					break;

				case "dueDateStart":
					that.formModel.dueDate.startDatePopupOpened = true;
					break;

				case "dueDateEnd":
					that.formModel.dueDate.endDatePopupOpened = true;
					break;

				case "shipDateStart":
					that.formModel.shipDate.startDatePopupOpened = true;
					break;

				case "shipDateEnd":
					that.formModel.shipDate.endDatePopupOpened = true;
					break;
			}
		}

		$scope.$watch(function() {
				return that.contentLoading;
			},
			function(value) {
				if (value) {
					modalService.showModal();
				} else {
					modalService.hideModal();
				}
			});
	}
})();