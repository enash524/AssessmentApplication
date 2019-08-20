function Address(addressId, addressLine1, addressLine2, city, stateProvinceCode, postalCode) {
	this.addressId = addressId;
	this.addressLine1 = addressLine1;
	this.addressLine2 = addressLine2;
	this.city = city;
	this.stateProvinceCode = stateProvinceCode;
	this.postalCode = postalCode;
	this.fullAddressHtml = [addressLine1, addressLine2, city, stateProvinceCode, postalCode]
		.filter(function(val) {
			if (val) {
				return val;
			}
		})
		.join("<br />");
}