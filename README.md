# Laf Deposu
Laf Deposu sizin girmiş olduğunuz harflerden oluşabilecek Türkçe kelimeleri bulmaya yarayan bir programdır.

Alfabede yer alan 29 harf dışında, özel bir harf olarak \* karakterini kullanabilirsiniz. Yıldız karakteri (\*) joker harf yerine geçip, alfabedeki tüm harfleri sizin için arayacaktır. Örneğin arama alanına ke* yazdığınızda karşınıza "kel, kek, kep, bek" sonuçları gelecektir.

Girdiğiniz harflerin sıralaması önemli değildir. kemal, ekmal, lemak ya da lamek arasınada program açısından bir fark yoktur. Yıldız karakterini de istediğiniz sırada yazabilirsiniz.

Aramalarınızda birden çok yıldız karakteri kullanabilirsiniz.

### Filtre

Filtre bölümü arama sonuçlarında karşınıza gelen listeyi kısıtlamanız için size yardımcı olur. Bu bölümde üç ayrı yere harflerinizi girebilirsiniz: Başlayan, İçeren ve Biten

#### Başlayan

Arama sonuçlarınız bu bölümde yazan harflerle başlayan kelimelerden oluşur. Örneğin **Arama** bölümüne kemal yazıp, **Başlayan** bölümüne k yazarsanız arama sonuçlarından sadece k ile başlayanlar size görünür ve sonuçlar **k**alem, **k**elam, **k**emal ve **k**ale olur. **Arama** bölümüne kemal ve **Başlayan** bölümüne kal yazarsanız bulacağınız sonuçlar **kal**em, **kal**e ve **kal** olur. Bu filtre alanında diğer alanlarda olduğu gibi virgül (,) kullanarak birden çok başlangıç filtresi oluşturabilirsiniz. Örneğin **Arama** bölümüne kemal yazıp, **Başlayan** bölümüne k,e yazarsanız arama sonuçlarından k ve e ile başlayanlar size görünür ve sonuçlar **e**mlak, **k**alem, **k**elam, **k**emal, **e**lma ve **k**ale olur.

#### İçeren

Arama sonuçlarınız bu bölümden yazan harfleri içeren kelimelerden oluşur. Örneğin keramet kelimesini aratıp, **İçeren** bölümüne mar yazarsanız bulacağınız sonuçlar e**mar**et, **mar**ket, e**mar**e, **mar**ke, **mar**k ve **mar**t olur. Bu alanda da kelimeler arasında virgül kullanarak arama sonuçarınızı birden çok içeren kriterine göre yapabilirsiniz.

#### Biten

Arama sonuçlarınız bu bölümde yazan harflerle biten kelimelerden oluşur. Örneğin kemal kelimesini aratıp, **Biten** bölümüne mal yazarsanız bulacağınız sonuçlar ke**mal** ve **mal** olacaktır. Bu alanda da virgül kullanarak arama sonuçlarınızı birden çok biten kriterien göre yapabilirsiniz.

##### Not

7 harfli kelimelerden osman ile başlayanları bulmak için **Arama** bölümüne *******, **Başlayan** bölümüne ise osman yazmanız yeterlidir. Bulacağınız sonuçlar **osman**lı ve **osman**i olacaktir. Bu şekilde aramalar veritabanının tamamında yapıldığı için daha uzun sürebilir.
