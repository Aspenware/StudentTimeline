var sampleData = (function () {
    var returnObj = {};

    returnObj.pets = [
        { Id: 1, Name: 'Kona', Description: 'Cutest Puppy Alive', Photo: 'img/kona.jpg', active: 1 },
        { Id: 2, Name: 'Spike', Description: 'Loves Tom and Jerry', Photo: 'img/spike.jpg', active: 1 },
        { Id: 3, Name: 'R2-D2', Description: 'Pal of C-3PO', Photo: 'img/R2-D2.jpg', active: 0 },
        { Id: 4, Name: 'Lassie', Description: 'Smart Cookie', Photo: 'img/lassie.jpg', active: 0 },
        { Id: 5, Name: 'Fido', Description: 'Mean and Nasty', Photo: 'img/fido.jpg', active: 0 },
        { Id: 6, Name: 'Garfield', Description: 'Hates Mondays', Photo: 'img/garfield.jpg', active: 1 },
        { Id: 7, Name: 'BB-8', Description: 'Cute little guy', Photo: 'img/bb-8.jpg', active: 0 }
    ];

    returnObj.tasks = [
        { Id: 1, Title: 'Feed AM', Pet: { Id: 1, Name: 'Kona', Description: 'Cutest Puppy Alive', Photo: 'img/kona.jpg', active: 1 } },
        { Id: 2, Title: 'Walk AM', Pet: { Id: 2, Name: 'Spike', Description: 'Loves Tom and Jerry', Photo: 'img/spike.jpg', active: 1 } },
        { Id: 3, Title: 'Fill Water AM', Pet: { Id: 1, Name: 'Kona', Description: 'Cutest Puppy Alive', Photo: 'img/kona.jpg', active: 1 } },
        { Id: 4, Title: 'Feed PM', Pet: { Id: 1, Name: 'Kona', Description: 'Cutest Puppy Alive', Photo: 'img/kona.jpg', active: 1 } },
        { Id: 5, Title: 'Walk PM', Pet: { Id: 2, Name: 'Spike', Description: 'Loves Tom and Jerry', Photo: 'img/spike.jpg', active: 1 } },
        { Id: 6, Title: 'Fill Water PM', Pet: { Id: 1, Name: 'Kona', Description: 'Cutest Puppy Alive', Photo: 'img/kona.jpg', active: 1 }}
    ];
    
    returnObj.friends = [
        { Id: 2, Name: 'Ike', Description: 'Spike\'s Owner', Photo: 'img/ike.jpg', active: 1 },
        { Id: 3, Name: 'C-3PO', Description: 'R2-D2\'s Owner', Photo: 'img/c-3po.jpg', active: 0 },
        { Id: 4, Name: 'Timmy', Description: 'Lassie saves him', Photo: 'img/timmy.jpg', active: 0 },
        { Id: 5, Name: 'Mylo', Description: 'Owner of Fido', Photo: 'img/user.jpg', active: 0 },
        { Id: 6, Name: 'Jon', Description: 'Overfeeds his cat', Photo: 'img/jon.jpg', active: 1 },
        { Id: 7, Name: 'Poe', Description: 'Best pilot in the resistance', Photo: 'img/poe.jpg', active: 0 }
    ];

    return returnObj;
}());