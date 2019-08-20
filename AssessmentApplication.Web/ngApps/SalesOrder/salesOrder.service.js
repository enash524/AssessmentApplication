(function () {
	"use strict";

	angular
		.module("salesOrderModule")
		.service("salesOrderService", ["$http", "$q", salesOrderService]);

	function salesOrderService($http, $q) {
		this.getSalesOrderDetail = function (salesOrderId) {
			var deferred = $q.defer(),
				config = {
					cache: false,
					headers: {
						"X-Requested-With": "XMLHttpRequest"
					},
					method: "GET",
					transformResponse: function(data) {
						if (data) {
							var result = [],
								arr = JSON.parse(data);

							angular.forEach(arr,
								function(value, key) {
									var detail = new SalesOrderDetail(value.lineTotal,
										value.name,
										value.orderQty,
										value.productNumber,
										value.unitPrice,
										value.unitPriceDiscount);

									result.push(detail);
								});

							return result;
						}

						return null;
					},
					url: "/api/SalesOrder/Detail/" + salesOrderId
				};

			$http(config)
				.then(function (response) {
						deferred.resolve(response);
					},
					function (err) {
						deferred.reject(err);
					});

			return deferred.promise;
		};

		this.getSalesOrderSearch = function (customerName, dueDate, orderDate, shipDate) {
			var deferred = $q.defer(),
				config = {
					cache: false,
					headers: {
						"X-Requested-With": "XMLHttpRequest"
					},
					method: "GET",
					params: {
						customerName: customerName,
						dueDateEnd: dueDate.selectedEndDate,
						dueDateStart: dueDate.selectedStartDate,
						orderDateEnd: orderDate.selectedEndDate,
						orderDateStart: orderDate.selectedStartDate,
						shipDateEnd: shipDate.selectedEndDate,
						shipDateStart: shipDate.selectedStartDate
					},
					transformResponse: function (data) {
						if (data) {
							var result = [],
								arr = JSON.parse(data);

							angular.forEach(arr,
								function (value, key) {
									var shipToAddress = new Address(value.shipToAddress.addressId,
											value.shipToAddress.addressLine1,
											value.shipToAddress.addressLine2,
											value.shipToAddress.city,
											value.shipToAddress.stateProvinceCode,
											value.shipToAddress.postalCode),
										person = new Person(value.person.businessEntityId,
											value.person.title,
											value.person.firstName,
											value.person.middleName,
											value.person.lastName,
											value.person.suffix),
										shipMethod = new ShipMethod(value.shipMethod.shipMethodId,
											value.shipMethod.name),
										header = new SalesOrderHeader(value.salesOrderId,
											value.accountNumber,
											value.freight,
											value.subTotal,
											value.taxAmt,
											value.totalDue,
											person,
											shipMethod,
											shipToAddress);

									result.push(header);
								});

							return result;
						}

						return null;
					},
					url: "/api/SalesOrder/Search"
				};

			$http(config)
				.then(function (response) {
						deferred.resolve(response);
					},
					function (err) {
						deferred.reject(err);
					});

			return deferred.promise;
		};
	}
})();