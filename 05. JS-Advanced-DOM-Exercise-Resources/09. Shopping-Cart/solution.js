function solve() {
   let arrProducts = [];
   let totalSum = 0;

   let addButtonElements = document.getElementsByClassName('add-product');

   Array.from(addButtonElements).map(x => x.addEventListener('click', addProduct));

   document.getElementsByClassName('checkout')[0].addEventListener('click', checkoutFun);


   function checkoutFun(e) {
      let index = 0;
      
      let uniqueValues = arrProducts.filter((p,i)=>i%2===0).filter(onlyUnique);

      arrProducts.forEach(product => {
         if (index%2!==0) {
            totalSum+=arrProducts[index];
         }

         index++;
      });

      document.querySelector('.shopping-cart textarea').innerHTML +=`You bought ${uniqueValues.join(', ')} for ${totalSum.toFixed(2)}.`;

      document.getElementsByClassName('checkout')[0].disabled = true;

      Array.from(addButtonElements).map(x =>x.disabled=true);
   }

   function addProduct(e) {
      let product = e.target.parentElement.parentElement;
      let title = product.children[1].children[0].innerHTML;
      let money = Number(product.children[3].innerHTML);

      arrProducts.push(title, money);
      document.querySelector('.shopping-cart textarea').innerHTML += `Added ${title} for ${money.toFixed(2)} to the cart.\n`;
   }

   function onlyUnique(value, index, self) {
      return self.indexOf(value) === index;
    }
}

