let {assert,expect} = require('chai');
let Warehouse = require('../warehouse.js');

describe('Warehouse class',function () {
    let wh;
    beforeEach(()=>{
        this.wh = new Warehouse(100);
    })

    describe('constructor function',function () {
        it('Should initialize properly',()=>{
            wh = new Warehouse(1000000);

            assert.equal(wh.capacity,1000000);
        });
    })

    describe('set capacity',function () {
        it('Should throw error if other than number is passed as input',()=>{
            assert.throw(()=> {wh.capacity = '1000000'},`Invalid given warehouse space`);
        });

        it('Should throw error if number passed as input is negative',()=>{
            assert.throw(()=> {wh.capacity = -1000000},`Invalid given warehouse space`);            
        });

        it('Should set capacity field to the passed input',()=>{
            wh.capacity = 10000;

            assert.equal(10000,wh.capacity);
        });
    })

    describe('addProduct function',function () {
        it('Should throw error if addedQuantity is negative',()=>{
            assert.throw(()=> {wh.addProduct('Drink','Rum',1000000)},`There is not enough space or the warehouse is already full`);
        });

        it('Should add product and set its quantity, plus adding it if exists',()=>{
            wh.addProduct('Food','Zelnik',10);
            wh.addProduct('Food','Zelnik',10);
            wh.addProduct('Food','Zelnik',10);
            
            // wrong logic!!!
            wh.addProduct('Food','Zelnik',-10);

            assert.equal(20,wh.availableProducts['Food']['Zelnik']);
        });

        it('Should return the correct type of stock',()=>{
            assert.equal(wh.availableProducts['Food'],wh.addProduct('Food','Zelnik',10));            
        });
    })

    describe('orderProducts function',function () {
        it('Should sort products by quantity',()=>{
           wh =  new Warehouse(100);
            wh.addProduct('Food','Zelnik',50);
            wh.addProduct('Food','Banica',10);
            wh.addProduct('Food','Patatnik',40);
            
            wh.orderProducts('Food');

            assert.equal(JSON.stringify({ Zelnik: 50, Patatnik: 40, Banica: 10 }),  JSON.stringify(wh.orderProducts('Food')));
        });
    })

    describe('occupiedCapacity function',function () {
        it('Should return 0 with no products',()=>{
           wh =  new Warehouse(100);
            assert.equal(0,wh.occupiedCapacity());
        });

        it('Should return the occupied capacity in the warehouse',()=>{
            wh.addProduct('Food','Zelnik',50);
            wh.addProduct('Food','Banica',10);
            wh.addProduct('Food','Patatnik',10);

            wh.addProduct('Drink','Vodka',30);
            
            assert.equal(100,wh.occupiedCapacity());
        });
    })

    describe('revision function',function () {
        it('Should return the warehouse is empty with no products',()=>{
           wh =  new Warehouse(100);
            assert.equal('The warehouse is empty',wh.revision());
        });

        it('Should return the occupied capacity in the warehouse',()=>{
            wh.addProduct('Food','Zelnik',50);
            wh.addProduct('Food','Banica',10);
            wh.addProduct('Food','Patatnik',10);

            wh.addProduct('Drink','Vodka',30);
            
            assert.equal(`Product type - [Food]\n- Zelnik 50\n- Banica 10\n- Patatnik 10\nProduct type - [Drink]\n- Vodka 30`
            ,wh.revision());
        });
    })

    describe('scrapeAProduct function',function () {
        it('Should throw product does not exists if no such product is found',()=>{                        
            expect(function(){wh.scrapeAProduct('Managua', 10000)}).to.throw(`Managua do not exists`);
        });

        it('Should set product value to 0 if quantity required is more than the available',()=>{
            wh.scrapeAProduct('Patatnik',100);

            let actual = wh.availableProducts.Food.Patatnik;

            assert.equal(0,actual);
        });

        it('Should decrease product value with the given quantity required',()=>{         
            wh.scrapeAProduct('Banica',5);

            let actual = wh.availableProducts.Food.Banica;

            assert.equal(5,actual);
        });

        it('Should not decrease product value with the given negative quantity required',()=>{         
            wh.scrapeAProduct('Banica',-5);

            let actual = wh.availableProducts.Food.Banica;

            assert.equal(10,actual);
        });
    })
})