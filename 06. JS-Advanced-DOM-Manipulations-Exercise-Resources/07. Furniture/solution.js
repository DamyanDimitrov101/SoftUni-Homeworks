function solve() {
  let [generateBtnElement, buyBtnElement] = document.getElementsByTagName('button');
  let [inputAreaElement, outputAreaElement] = document.getElementsByTagName('textarea');


  generateBtnElement.addEventListener('click', onGenerate);
  buyBtnElement.addEventListener('click', onBuy);

  function onBuy(e) {
    let [products,prices,factors] = [[],[],[]];
    let productToBuy = [...document.querySelectorAll('tbody tr')].forEach(tr=>{
                      if (tr.querySelector('input').checked) {
                      
                        products.push(tr.querySelectorAll('p')[0].textContent);
                        prices.push(Number(tr.querySelectorAll('p')[1].textContent));
                        factors.push(Number(tr.querySelectorAll('p')[2].textContent));
                      }
    
  });

      outputAreaElement.textContent += `Bought furniture: ${products.join(', ')}\n`;
      outputAreaElement.textContent += `Total price: ${Number(prices.reduce((a,x)=>a+x,0)).toFixed(2)}\n`;
      outputAreaElement.textContent += `Average decoration factor: ${(Number(factors.reduce((a,x)=>a+x,0))/factors.length).toFixed(2)}`;
  }

  function onGenerate(e) {
    let arrInputs = JSON.parse(inputAreaElement.value);
    //console.log(current);

    for (const product of arrInputs) {
      let row = ` <tr>
                    <td>
                        <img
                            src="${product.img}">
                    </td>
                    <td>
                        <p>${product.name}</p>
                    <td>
                        <p>${product.price}</p>
                    </td>
                    <td>
                        <p>${product.decFactor}</p>
                    </td>
                    <td>
                        <input type="checkbox" />
                    </td>
                  </tr>`;
        let rowElement = document.createElement('tr');
        rowElement.insertAdjacentHTML('beforeend',row);
        
      document.getElementsByTagName('tbody')[0].appendChild(rowElement);
    }
  }
}