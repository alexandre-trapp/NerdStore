Funcionalidade: Pedido - Adicionar item ao pedido
	Como um usuário
	Eu desejo adicionar um item no pedido
	Para que eu possa comprá-lo posteriormente

Cenário: Adicionar item com sucesso a um novo pedido
Dado que um produto esteja disponível no estoque
Quando O usuário adicionar uma ou mais unidades ao pedido
Então O valor total do pedido será exatamente o valor do item adicionado multiplicado pela quantidade

Cenário: Adicionar item acima do limite
Dado que um produto esteja disponível no estoque
Quando O usuário adicionar um item acima da quantidade máxima permitida
Então Receberá uma mensagem de erro mencionando que foi ultrapassada a quantidade limite