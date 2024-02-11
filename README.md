# Электронное меню ресторана
Бэкэнд для Web приложения, в котором можно просматривать блюда ресторана, создавать заказы в ресторане

Требования:
* На странице должен быть список позиций (блюд) в меню
* У позиций есть характеристики:
  * Название
  * Картинка блюда
  * Цена
  * Вес
  * Калорийность (на 100г)
  * Является ли блюдо полностью подходящим для веганов
  * Краткий состав
* Позиции могут быть отсортированы по:
  * Названию
  * Цене
  * Калорийности
* Пользователь должен иметь возможность залогиниться на сайте
* Залогиненный пользователь может составлять электронный заказ и делать с ним следующие действия:
  * Бронировать столик для заказа в указанную дату
  * Добавлять / Удалять блюдо в заказ
  * Смотреть итоговую стоимость
  * Оплачивать заказ
* Должна быть предусмотренна возможность входа на сайт с правами администратора
* Администратор должен иметь следующие возможности:
  * Добавлять / Изменять / Удалять позиции в меню

Модель данных:
![тут должна быть модель](ElectronicMenu.jpg)

### TODO
* Доделать функционал с заказами
