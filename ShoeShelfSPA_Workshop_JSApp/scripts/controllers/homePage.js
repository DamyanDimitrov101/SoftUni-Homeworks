import {shoes, loadPartials, handleError} from '../util.js';

export function homePage (context) {
    shoes.get()
        .then(function (collection) {
            let arr = collection.docs.map(x => { return { ...x.data(), offerId: x.o_.id } });

            context.shoes = arr;
        })
        .then(function () {
            loadPartials(context)
                .then(function () {
                    this.partial('./templates/home.hbs');
                });
        })
        .catch(handleError);
}