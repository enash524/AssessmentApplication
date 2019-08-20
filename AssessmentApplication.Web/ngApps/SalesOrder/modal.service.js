(function() {
	"use strict";

	angular
		.module("salesOrderModule")
		.service("modalService", ["$uibModal", modalService]);

	function modalService($uibModal) {
		var modalDefaults = {
				animation: true,
				backdrop: "static",
				keyboard: false,
				size: "sm",
				templateUrl: "/ngApps/SalesOrder/Views/NowLoading.html"
			},
			$uibModalInstance = null;

		this.showModal = function() {
			$uibModalInstance = $uibModal.open(modalDefaults);
		};

		this.hideModal = function() {
			if ($uibModalInstance) {
				$uibModalInstance.close();
			}
		};
	}
})();