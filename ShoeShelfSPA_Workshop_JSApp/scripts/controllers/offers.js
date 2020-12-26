import {loadPartials, getUserData, getOffer, 
        handleError, handleMessage, shoes} 
        from '../util.js';

export function createOfferPage (context) {
    loadPartials(context)
        .then(function () {
            this.partial('./templates/createOffer.hbs');
        });
}

export function detailsPage (context) {
    let { id } = context.params;
    let { uid } = getUserData();

    if (id[0] == ':') {
        id = id.slice(1);
    }

    getOffer(id)
        .get()
        .then(function (doc) {

            let offer = doc.data();

            context.offer = offer;
            context.id = id

            if (offer.creator === uid) {
                context.creator = true;
            }

            if (offer.buyers.includes(uid)) {
                context.isBought = true;
            }
        })
        .then(function () {
            loadPartials(context)
                .then(function () {

                    this.partial('../templates/details.hbs');

                })
        });
}

export function editPage (context) {

    getOffer(context.params.id.slice(1))
        .get()
        .then(function (doc) {
            let offer = doc.data();

            offer.id = context.params.id.slice(1);

            context.offer = offer;
        })
        .then(function () {
            loadPartials(context)
                .then(function () {
                    this.partial('./templates/edit.hbs');
                });
        })
        .catch(handleError);

}

export function buy (context) {
    let { id } = context.params;
    shoes
        .doc(id.slice(1))
        .get()
        .then(function (doc) {
            let { uid } = getUserData();
            let arr = Array.from(doc.data().buyers);

            if (!arr.includes(uid)) {
                arr.push(uid);
            }

            shoes.doc(id.slice(1))
                .set({ buyers: arr }, { merge: true })
                .then(function () {
                    handleMessage('Bought!');
                    context.redirect(`/details/${id}`);
                });
        })
}

export function deleteOffer (context) {
    let { id } = context.params;
    id = id.slice(1);
    shoes.doc(id)
        .delete()
        .then(function () {
            context.redirect('/home');
        })
        .catch(handleError);
}

export function createOfferPost (context) {
    let { name, price, description, imageURL, brand } = context.params;

    const { uid } = getUserData();

    if (name == '' || price == '' || description == '' || imageURL == '' || brand == '') {
        return;
    }

    shoes.add({
        name,
        price,
        description,
        imageURL,
        brand,
        creator: uid,
        buyers: []
    })
        .then(function (docRef) {
            context.redirect('/home');
        })
        .catch(handleError);

    this.redirect('/home');
}

export function editOfferPost (context) {
    let { name, price, description, imageURL, brand } = context.params;

    let offerId = context.params.id.slice(1);

    getOffer(offerId)
        .get()
        .then(function (doc) {
            context.offer = doc.data();
        })
        .then(function () {
            let offer = { name, price, description, imageURL, brand, creator: context.offer.creator };

            getOffer(offerId)
                .set(offer)
                .then(function (doc) {

                    handleMessage('Edited successfully!');
                    context.redirect(`/details/:${offerId}`);
                });
        })
        .catch(handleError);
}