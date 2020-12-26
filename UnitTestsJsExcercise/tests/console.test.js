let {assert,expect} = require('chai');
let Console = require('../console.js');

describe('Console class',function () {
    it('Should return the object as string if no placeholders are passed and the type of input is object',()=>{
        assert.equal("{\"na\":5}",Console.writeLine({na:5}));
    });

    it('Should return the message if no placeholders are passed',()=>{
        assert.equal("The sum of and is ",Console.writeLine("The sum of and is "));
    });

    it('Should throw error if placeholders are added but the input is not string',()=>{        
        expect(()=> {Console.writeLine(455,5)}).to.throw("No string format given!");
    });

    it('Should throw error if placeholders are added but they are not equal to the arguments passed',()=>{        
        expect(()=> {Console.writeLine("{1} {2}",5,3,4)}).to.throw("Incorrect amount of parameters given!");

        expect(()=> {Console.writeLine("{1} {2}",4)}).to.throw("Incorrect amount of parameters given!");        
    });


    it('Should throw error if placeholders are added but they are not the same to the arguments passed',()=>{        
        expect(()=> {Console.writeLine("{3} {2}",5,3)}).to.throw("Incorrect placeholders given!");

        expect(()=> {Console.writeLine("{1} {12}",5,3)}).to.throw("Incorrect placeholders given!");
    });

    it('Should replace the placeholders as expected',()=>{
        assert.equal(Console.writeLine("The sum of {0} and {1} is {2}", 3, 4, 7),"The sum of 3 and 4 is 7")
    });
})