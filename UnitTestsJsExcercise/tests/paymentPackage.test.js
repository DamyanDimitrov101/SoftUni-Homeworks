let {assert} = require('chai');
let PaymentPackage = require('../paymentPackage.js');

describe('PaymentPackage class',function () {
    let payPack;
    beforeEach(()=>{
        payPack = new PaymentPackage('HR Services',1500);
    })

    describe('constructor',function () {
        it('Should initialize instance properly',()=>{
            payPack = new PaymentPackage('HR Services',1500);

            assert.equal('HR Services',payPack.name);
            assert.equal(1500,payPack.value);
            assert.equal(20,payPack.VAT);
            assert.equal(true,payPack.active);
        });
    })

    describe('set name',function () {
        it('Should throw error with non string input',()=>{
            assert.throw(()=> {payPack.name = 56},'Name must be a non-empty string');
        });

        it('Should throw error with empty string input',()=>{
            assert.throw(()=> {payPack.name = ''},'Name must be a non-empty string');
        });

        it('Should work properly with string input',()=>{
            payPack.name = 'G'
            assert.equal(payPack.name,'G');
        });
    })

    describe('set value',function () {
        it('Should throw error with non number input',()=>{
            assert.throw(()=> {payPack.value = '52'},'Value must be a non-negative number');
        });

        it('Should throw error with negative input',()=>{
            assert.throw(()=> {payPack.value = -85},'Value must be a non-negative number');
        });

        it('Should work properly with number input',()=>{
            payPack.value = 5000000;
            assert.equal(payPack.value,5000000);
        });
    })


    describe('set VAT',function () {
        it('Should throw error with non number input',()=>{
            assert.throw(()=> {payPack.VAT = '52'},'VAT must be a non-negative number');
        });

        it('Should throw error with negative input',()=>{
            assert.throw(()=> {payPack.VAT = -85},'VAT must be a non-negative number');
        });

        it('Should work properly with number input',()=>{
            payPack.VAT = 5000000;
            assert.equal(payPack.VAT,5000000);
        });
    })

    describe('set active',function () {
        it('Should throw error with non boolean input',()=>{
            assert.throw(()=> {payPack.active = '52'},'Active status must be a boolean');
        });

        it('Should work properly with boolean input',()=>{
            payPack.active = false;
            assert.equal(payPack.active,false);
        });
    })

    describe('toString function',function () {        
        it('Should work properly',()=>{
            let expectedOutput = 'Package: HR Services\n- Value (excl. VAT): 1500\n- Value (VAT 20%): 1800';
            assert.equal(payPack.toString(),expectedOutput);
        });
    })
})