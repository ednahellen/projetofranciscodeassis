-- APAGANDO O BANCO DE DADOS
DROP DATABASE IF EXISTS dbfrancisco;

-- CRIANDO O BANCO DE DADOS
CREATE DATABASE dbfrancisco;
USE dbfrancisco;

ALTER DATABASE dbfrancisco 
CHARACTER SET utf8mb4 
COLLATE utf8mb4_general_ci;


-- TABELA DE VOLUNTÁRIOS

CREATE TABLE tbVoluntarios(
    codVol INT NOT NULL AUTO_INCREMENT,
    nome VARCHAR(100) NOT NULL,
    telCel VARCHAR(15),
    cpf VARCHAR(14) NULL UNIQUE,
    cep VARCHAR(9),
    rua VARCHAR(100),
    numero VARCHAR(5),
    complemento VARCHAR(100),
    bairro VARCHAR(100),
    cidade VARCHAR(100),
    estado VARCHAR(2),
    ativo BOOLEAN DEFAULT TRUE,
    foto LONGBLOB,
    PRIMARY KEY(codVol)
);

-- TABELA DE USUÁRIOS

CREATE TABLE tbUsuarios(
    codUsu INT NOT NULL AUTO_INCREMENT,
    usuario VARCHAR(100) NOT NULL UNIQUE,
    senha VARCHAR(100) NOT NULL,
    tipo ENUM('ADMIN','USER') DEFAULT 'USER',
    ativo BOOLEAN DEFAULT TRUE,
    codVol INT NOT NULL,
    PRIMARY KEY(codUsu),
    FOREIGN KEY(codVol) REFERENCES tbVoluntarios(codVol)
);

-- TABELA DE CLIENTES

CREATE TABLE tbClientes(
    codCli INT NOT NULL AUTO_INCREMENT,  
    nome VARCHAR(100) NOT NULL,
    cpf VARCHAR(14) UNIQUE,
    cnpj VARCHAR(18) UNIQUE,
    cep VARCHAR(9),
    rua VARCHAR(100),
    numero VARCHAR(5),
    complemento VARCHAR(100),
    bairro VARCHAR(100),
    cidade VARCHAR(100),
    estado VARCHAR(2),
    telCel VARCHAR(15),
    referencia VARCHAR(200) NOT NULL,
    PRIMARY KEY(codCli)
);

-- TABELA DE ORIGEM DE DOAÇÃO (FORNECEDORES/DOADORES)

CREATE TABLE tbOrigemDoacao(
    codOri INT NOT NULL AUTO_INCREMENT,  
    nome VARCHAR(100) NOT NULL,
    cpf VARCHAR(14) UNIQUE,
    cnpj VARCHAR(18) UNIQUE,
    cep VARCHAR(9),
    rua VARCHAR(100),
    numero VARCHAR(5),
    complemento VARCHAR(100),
    bairro VARCHAR(100),
    cidade VARCHAR(100),
    estado VARCHAR(2),
    telCel VARCHAR(15),
    referencia VARCHAR(200),
    PRIMARY KEY(codOri)
);

-- TABELA DE UNIDADES DE MEDIDA

CREATE TABLE tbUnidades(
    codUni INT NOT NULL AUTO_INCREMENT,  
    descricao VARCHAR(20) NOT NULL UNIQUE,
    PRIMARY KEY(codUni)
);

-- TABELA DE LISTA DE PRODUTOS

CREATE TABLE tbLista(
    codList INT NOT NULL AUTO_INCREMENT,  
    descricao VARCHAR(100) NOT NULL,
    peso INT NOT NULL,
    unidade VARCHAR(20) NOT NULL,
    codUni INT NOT NULL,
    PRIMARY KEY(codList),
    FOREIGN KEY(codUni) REFERENCES tbUnidades(codUni)
);

-- TABELA DE JORNAL

CREATE TABLE tbJornal(
    codJor INT NOT NULL AUTO_INCREMENT,
    titulo VARCHAR(100) NOT NULL,
    dataDePublicacao DATETIME NOT NULL,
    descricao VARCHAR(10000) NOT NULL, 
    foto LONGBLOB NOT NULL,
    tema VARCHAR(100) NOT NULL,
    email VARCHAR(100),
    nome VARCHAR(100),
    codUsu INT NOT NULL,
    PRIMARY KEY(codJor),
    FOREIGN KEY(codUsu) REFERENCES tbUsuarios(codUsu)
);

-- TABELA DE FALE CONOSCO

CREATE TABLE tbFaleConosco(
    codFaleConosco INT NOT NULL AUTO_INCREMENT,
    nome VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL,
    assunto VARCHAR(100),
    mensagem VARCHAR(200) NOT NULL,
    codUsu INT NOT NULL,
    PRIMARY KEY(codFaleConosco),
    FOREIGN KEY(codUsu) REFERENCES tbUsuarios(codUsu)
);

-- TABELA DE PRODUTOS

CREATE TABLE tbProdutos(
    codProd INT NOT NULL AUTO_INCREMENT,  
    descricao VARCHAR(100) NOT NULL,
    -- quantidade INT NOT NULL,  -- REMOVIDA (substituída por estoqueAtual)
    peso DECIMAL(10,3) NOT NULL,  -- MODIFICADO para DECIMAL
    unidade VARCHAR(20) NOT NULL,
    codBar VARCHAR(13) NOT NULL UNIQUE,  -- MODIFICADO para UNIQUE
    dataDeEntrada DATETIME NOT NULL,
    dataDeValidade DATETIME NOT NULL,
    dataLimiteDeSaida DATETIME,
    -- NOVOS CAMPOS
    estoqueAtual INT NOT NULL DEFAULT 0,  -- Controle de estoque atual
    estoqueMinimo INT DEFAULT 5,  -- Alerta de estoque baixo
    localizacao VARCHAR(50),  -- Local no depósito (prateleira, corredor)
    observacao VARCHAR(200),  -- Observações gerais
    -- CAMPOS EXISTENTES (mantidos)
    codUsu INT NOT NULL,
    codOri INT NOT NULL,
    codList INT NOT NULL,
    PRIMARY KEY(codProd),
    UNIQUE INDEX idx_codBar (codBar),  -- Índice para busca rápida
    INDEX idx_validade (dataDeValidade),  -- Índice para consultas por validade
    INDEX idx_estoque (estoqueAtual),  -- Índice para consultas de estoque
    FOREIGN KEY(codUsu) REFERENCES tbUsuarios(codUsu),
    FOREIGN KEY(codOri) REFERENCES tbOrigemDoacao(codOri),
    FOREIGN KEY(codList) REFERENCES tbLista(codList)
);


ALTER TABLE tbProdutos MODIFY codBar VARCHAR(13) NULL;

-- TABELA DE CESTAS

CREATE TABLE tbCestas(
    codCes INT NOT NULL AUTO_INCREMENT,
    dataDeSaida DATE NOT NULL,
    quantidade INT NOT NULL,
    -- NOVOS CAMPOS
    motivo VARCHAR(100) DEFAULT 'Doação',  -- Motivo da saída
    observacao VARCHAR(200),  -- Observações adicionais
    -- CAMPOS EXISTENTES
    codProd INT NOT NULL,
    codUsu INT NOT NULL,
    dataDeMontagem DATETIME NOT NULL,
    codCli INT NOT NULL,
    PRIMARY KEY(codCes),
    INDEX idx_dataSaida (dataDeSaida),  -- Índice para relatórios por data
    FOREIGN KEY(codProd) REFERENCES tbProdutos(codProd),
    FOREIGN KEY(codUsu) REFERENCES tbUsuarios(codUsu),
    FOREIGN KEY(codCli) REFERENCES tbClientes(codCli)
);


-- VIEWS (VISÕES) PARA FACILITAR CONSULTAS

-- APAGANDO AS VIEWS EXISTENTES (se houver)
DROP VIEW IF EXISTS vw_estoque_atual;
DROP VIEW IF EXISTS vw_estoque_baixo;
DROP VIEW IF EXISTS vw_historico_entradas;
DROP VIEW IF EXISTS vw_historico_saidas;
DROP VIEW IF EXISTS vw_produtos_por_validade;

-- =====================================================
-- VIEWS (VISÕES) CORRIGIDAS
-- =====================================================

-- VIEW: Estoque atual com informações completas
CREATE VIEW vw_estoque_atual AS
SELECT 
    p.codProd,
    p.descricao,
    p.estoqueAtual,
    p.estoqueMinimo,  -- INCLUÍDO esta coluna
    p.unidade,
    p.dataDeValidade,
    p.dataLimiteDeSaida,
    p.localizacao,
    DATEDIFF(p.dataDeValidade, NOW()) as dias_para_vencer,
    CASE 
        WHEN DATEDIFF(p.dataDeValidade, NOW()) <= 0 THEN 'Vencido'
        WHEN DATEDIFF(p.dataDeValidade, NOW()) <= 7 THEN 'Vence em 7 dias'
        WHEN DATEDIFF(p.dataDeValidade, NOW()) <= 15 THEN 'Vence em 15 dias'
        WHEN DATEDIFF(p.dataDeValidade, NOW()) <= 30 THEN 'Vence em 30 dias'
        ELSE 'OK'
    END as status_validade,
    CASE
        WHEN p.estoqueAtual <= p.estoqueMinimo THEN 'Estoque Baixo'
        WHEN p.estoqueAtual = 0 THEN 'Sem Estoque'
        ELSE 'Normal'
    END as status_estoque,
    o.nome as origem_doacao,
    u.nome as responsavel_cadastro
FROM tbProdutos p
LEFT JOIN tbOrigemDoacao o ON p.codOri = o.codOri
LEFT JOIN tbVoluntarios u ON p.codUsu = u.codVol;

-- VIEW: Produtos com estoque baixo 

CREATE VIEW vw_estoque_baixo AS
SELECT * FROM tbProdutos 
WHERE estoqueAtual <= estoqueMinimo 
AND estoqueAtual > 0;  -- Exclui produtos com estoque zero

-- VIEW: Produtos com estoque baixo 
CREATE VIEW vw_estoque_baixo_completo AS
SELECT 
    p.codProd,
    p.descricao,
    p.estoqueAtual,
    p.estoqueMinimo,
    (p.estoqueMinimo - p.estoqueAtual) as quantidade_faltante,
    p.unidade,
    p.dataDeValidade,
    DATEDIFF(p.dataDeValidade, NOW()) as dias_para_vencer,
    o.nome as origem_doacao,
    p.localizacao
FROM tbProdutos p
LEFT JOIN tbOrigemDoacao o ON p.codOri = o.codOri
WHERE p.estoqueAtual <= p.estoqueMinimo
AND p.estoqueAtual > 0
ORDER BY (p.estoqueMinimo - p.estoqueAtual) DESC;

-- VIEW: Histórico de entradas

CREATE VIEW vw_historico_entradas AS
SELECT 
    p.codProd,
    p.descricao,
    p.estoqueAtual as quantidade_entrada,  
    p.dataDeEntrada,
    p.dataDeValidade,
    o.nome as origem,
    u.nome as responsavel
FROM tbProdutos p
LEFT JOIN tbOrigemDoacao o ON p.codOri = o.codOri
LEFT JOIN tbVoluntarios u ON p.codUsu = u.codVol
ORDER BY p.dataDeEntrada DESC;

-- VIEW: Histórico de saídas

CREATE VIEW vw_historico_saidas AS
SELECT 
    c.codCes,
    p.descricao as produto,
    c.quantidade,
    c.dataDeSaida,
    c.motivo,
    cl.nome as cliente,
    u.nome as responsavel,
    c.observacao
FROM tbCestas c
INNER JOIN tbProdutos p ON c.codProd = p.codProd
INNER JOIN tbClientes cl ON c.codCli = cl.codCli
LEFT JOIN tbVoluntarios u ON c.codUsu = u.codVol
ORDER BY c.dataDeSaida DESC;

-- VIEW: Produtos por validade

CREATE VIEW vw_produtos_por_validade AS
SELECT 
    codProd,
    descricao,
    estoqueAtual,
    estoqueMinimo,
    unidade,
    dataDeValidade,
    DATEDIFF(dataDeValidade, NOW()) as dias_para_vencer,
    CASE 
        WHEN DATEDIFF(dataDeValidade, NOW()) <= 0 THEN 'Vencido'
        WHEN DATEDIFF(dataDeValidade, NOW()) <= 7 THEN 'Vence em 7 dias'
        WHEN DATEDIFF(dataDeValidade, NOW()) <= 15 THEN 'Vence em 15 dias'
        WHEN DATEDIFF(dataDeValidade, NOW()) <= 30 THEN 'Vence em 30 dias'
        ELSE 'OK'
    END as status_validade,
    localizacao
FROM tbProdutos
WHERE estoqueAtual > 0
ORDER BY 
    CASE 
        WHEN DATEDIFF(dataDeValidade, NOW()) <= 0 THEN 1  -- Vencidos primeiro
        WHEN DATEDIFF(dataDeValidade, NOW()) <= 7 THEN 2  -- Próximos 7 dias
        WHEN DATEDIFF(dataDeValidade, NOW()) <= 15 THEN 3 -- Próximos 15 dias
        WHEN DATEDIFF(dataDeValidade, NOW()) <= 30 THEN 4 -- Próximos 30 dias
        ELSE 5
    END,
    dataDeValidade;

-- VIEW: Resumo do estoque

CREATE VIEW vw_resumo_estoque AS
SELECT 
    COUNT(DISTINCT codProd) as total_produtos,
    SUM(estoqueAtual) as total_itens_estoque,
    COUNT(CASE WHEN estoqueAtual <= estoqueMinimo AND estoqueAtual > 0 THEN 1 END) as produtos_estoque_baixo,
    COUNT(CASE WHEN estoqueAtual = 0 THEN 1 END) as produtos_sem_estoque,
    COUNT(CASE WHEN DATEDIFF(dataDeValidade, NOW()) <= 30 AND DATEDIFF(dataDeValidade, NOW()) > 0 THEN 1 END) as produtos_vencer_30_dias,
    COUNT(CASE WHEN DATEDIFF(dataDeValidade, NOW()) <= 0 THEN 1 END) as produtos_vencidos
FROM tbProdutos;

-- VIEW: Movimentações completas (NOVA - une entradas e saídas)

CREATE VIEW vw_movimentacoes_completas AS
SELECT 
    'ENTRADA' as tipo_movimentacao,
    p.descricao as produto,
    p.estoqueAtual as quantidade,
    p.dataDeEntrada as data_movimentacao,
    o.nome as origem_destino,
    u.nome as responsavel,
    p.observacao
FROM tbProdutos p
LEFT JOIN tbOrigemDoacao o ON p.codOri = o.codOri
LEFT JOIN tbVoluntarios u ON p.codUsu = u.codVol

UNION ALL

SELECT 
    'SAÍDA' as tipo_movimentacao,
    p.descricao as produto,
    c.quantidade,
    c.dataDeSaida as data_movimentacao,
    cl.nome as origem_destino,
    u.nome as responsavel,
    c.observacao
FROM tbCestas c
INNER JOIN tbProdutos p ON c.codProd = p.codProd
INNER JOIN tbClientes cl ON c.codCli = cl.codCli
LEFT JOIN tbVoluntarios u ON c.codUsu = u.codVol

ORDER BY data_movimentacao DESC;


-- PROCEDURE: Registrar entrada de produto 

DELIMITER $$
CREATE PROCEDURE sp_registrar_entrada(
    IN p_codBar VARCHAR(13),
    IN p_quantidade INT,
    IN p_dataValidade DATETIME,
    IN p_codUsu INT,
    IN p_codOri INT,
    IN p_observacao VARCHAR(200)
)
BEGIN
    DECLARE v_codProd INT;
    DECLARE v_estoqueAtual INT;
    DECLARE v_descricao VARCHAR(100);

    -- Verifica se produto já existe
    SELECT codProd, estoqueAtual, descricao INTO v_codProd, v_estoqueAtual, v_descricao
    FROM tbProdutos 
    WHERE codBar = p_codBar;
    
    IF v_codProd IS NOT NULL THEN
        -- Atualiza produto existente
        UPDATE tbProdutos SET
            estoqueAtual = estoqueAtual + p_quantidade,
            dataDeEntrada = NOW(),
            dataDeValidade = p_dataValidade,
            observacao = CONCAT(IFNULL(observacao, ''), ' | ', p_observacao)
        WHERE codProd = v_codProd;
       
        SELECT CONCAT('Estoque de "', v_descricao, '" atualizado. Novo total: ', 
                     v_estoqueAtual + p_quantidade) as mensagem;
    ELSE
        SIGNAL SQLSTATE '45000' 
        SET MESSAGE_TEXT = 'Produto não encontrado. Use INSERT diretamente para novos produtos.';
    END IF;
END$$
DELIMITER ;


-- PROCEDURE: Registrar saída de produto

DELIMITER $$
CREATE PROCEDURE sp_registrar_saida(
    IN p_codProd INT,
    IN p_quantidade INT,
    IN p_codCli INT,
    IN p_codUsu INT,
    IN p_motivo VARCHAR(100),
    IN p_observacao VARCHAR(200)
)
BEGIN
    DECLARE v_estoqueAtual INT;
    DECLARE v_descricao VARCHAR(100);
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SELECT 'Erro ao registrar saída. Transação cancelada.' as mensagem;
    END;
    
    -- Verifica estoque
    SELECT estoqueAtual, descricao INTO v_estoqueAtual, v_descricao
    FROM tbProdutos 
    WHERE codProd = p_codProd;
    
    IF v_estoqueAtual < p_quantidade THEN
        SIGNAL SQLSTATE '45000' 
        SET MESSAGE_TEXT = 'Estoque insuficiente';
    ELSE
        -- Inicia transação
        START TRANSACTION;
        
        -- Registra saída na cesta
        INSERT INTO tbCestas 
            (dataDeSaida, quantidade, codProd, codUsu, dataDeMontagem, codCli, motivo, observacao)
        VALUES 
            (NOW(), p_quantidade, p_codProd, p_codUsu, NOW(), p_codCli, p_motivo, p_observacao);
        
        -- Atualiza estoque
        UPDATE tbProdutos 
        SET estoqueAtual = estoqueAtual - p_quantidade 
        WHERE codProd = p_codProd;
        
        -- Confirma transação
        COMMIT;
        
        SELECT CONCAT('Saída de ', p_quantidade, ' unidades de "', v_descricao, 
                     '" registrada com sucesso. Estoque restante: ', 
                     v_estoqueAtual - p_quantidade) as mensagem;
    END IF;
END$$
DELIMITER ;


-- -- Inserir voluntário admin
-- INSERT INTO tbVoluntarios
-- (nome, telCel, cpf, cep, rua, numero, complemento, bairro, cidade, estado)
-- VALUES
-- ('Admin','(11)90000-0000','000.000.000-00','00000-000','Grupo Francisco','000','','Jd.Francisco','São Paulo','SP');

-- -- Inserir usuário admin
-- INSERT INTO tbUsuarios
-- (usuario, senha, tipo, codVol)
-- VALUES
-- ('admin','123','ADMIN',1);

-- -- Inserir unidades de medida
-- INSERT INTO tbUnidades (descricao) VALUES 
-- ('UN'),  -- Unidade
-- ('KG'),  -- Quilograma
-- ('G'),   -- Grama
-- ('L'),   -- Litro
-- ('ML'),  -- Mililitro
-- ('PC'),  -- Peça
-- ('PCT'), -- Pacote
-- ('CX');  -- Caixa

-- -- Inserir alguns itens na lista de produtos
-- INSERT INTO tbLista (descricao, peso, unidade, codUni) VALUES
-- ('Arroz Branco', 5, 'KG', 2),
-- ('Feijão Carioca', 1, 'KG', 2),
-- ('Macarrão Espaguete', 500, 'G', 3),
-- ('Farinha de Trigo', 1, 'KG', 2),
-- ('Açúcar Refinado', 1, 'KG', 2),
-- ('Óleo de Soja', 900, 'ML', 5),
-- ('Leite em Pó', 400, 'G', 3),
-- ('Café em Pó', 500, 'G', 3),
-- ('Sabonete', 90, 'G', 3),
-- ('Creme Dental', 90, 'G', 3);


INSERT INTO tbVoluntarios
(nome, telCel, cpf, cep, rua, numero, complemento, bairro, cidade, estado)
VALUES
('Admin','(11)90000-0000','000.000.000-00','00000-000','Grupo Francisco','000','','Jd.Francisco','São Paulo','SP');
-- ('João Silva','(11)91111-1111','111.111.111-01','','','','','','','');
-- ('Maria Souza','',NULL,'','','','','','','');
-- INSERT INTO tbVoluntarios
-- (nome, telCel, cpf, cep, rua, numero, complemento, bairro, cidade, estado, ativo)
-- VALUES
-- ('Carlos Pereira','1193333-3333','111.111.111-03','03003-000','Rua C','30','','Mooca','São Paulo','SP', 0),
-- ('Ana Costa','1194444-4444','111.111.111-04','04004-000','Rua D','40','Casa','Ipiranga','São Paulo','SP', 0);
-- ('Lucas Lima','1195555-5555','111.111.111-05','05005-000','Rua E','50','','Santana','São Paulo','SP'),
-- ('Fernanda Rocha','1196666-6666','111.111.111-06','06006-000','Rua F','60','Fundos','Penha','São Paulo','SP'),
-- ('Bruno Martins','1197777-7777','111.111.111-07','07007-000','Rua G','70','','Tatuapé','São Paulo','SP'),
-- ('Patricia Alves','1198888-8888','111.111.111-08','08008-000','Rua H','80','Bloco B','Lapa','São Paulo','SP'),
-- ('Rafael Gomes','1199999-9999','111.111.111-09','09009-000','Rua I','90','','Pinheiros','São Paulo','SP'),
-- ('Juliana Ribeiro','1181111-1111','111.111.111-10','10010-000','Rua J','100','Apto 12','Perdizes','São Paulo','SP'),
-- ('Daniel Santos','1182222-2222','111.111.111-11','11011-000','Rua K','110','','Vila Mariana','São Paulo','SP'),
-- ('Camila Torres','1183333-3333','111.111.111-12','12012-000','Rua L','120','Casa','Jabaquara','São Paulo','SP'),
-- ('Eduardo Nunes','1184444-4444','111.111.111-13','13013-000','Rua M','130','','Butantã','São Paulo','SP'),
-- ('Renata Freitas','1185555-5555','111.111.111-14','14014-000','Rua N','140','Apto 3','Morumbi','São Paulo','SP'),
-- ('Thiago Barros','1186666-6666','111.111.111-15','15015-000','Rua O','150','','Campo Limpo','São Paulo','SP'),
-- ('Aline Pacheco','1187777-7777','111.111.111-16','16016-000','Rua P','160','Bloco C','Itaquera','São Paulo','SP'),
-- ('Marcos Teixeira','1188888-8888','111.111.111-17','17017-000','Rua Q','170','','Osasco','Osasco','SP'),
-- ('Bianca Lopes','1189999-9999','111.111.111-18','18018-000','Rua R','180','Casa','Guarulhos','Guarulhos','SP'),
-- ('Felipe Araujo','1171111-1111','111.111.111-19','19019-000','Rua S','190','','Santo Amaro','São Paulo','SP'),
-- ('Larissa Mendes','1172222-2222','111.111.111-20','20020-000','Rua T','200','Apto 5','Interlagos','São Paulo','SP');



INSERT INTO tbUsuarios
(usuario, senha, tipo, codVol)
VALUES
('admin','123','ADMIN',1);
-- ('joao.silva','123','USER',2);
-- ('maria.souza','123','USER',3);
-- INSERT INTO tbUsuarios
-- (usuario, senha, tipo, salt, codVol, ativo)
-- VALUES
-- ('carlos.pereira','123','USER','salt04',4,0),
-- ('ana.costa','123','USER','salt05',5,0);
-- ('lucas.lima','123','USER','salt06',6),
-- ('fernanda.rocha','123','USER','salt07',7),
-- ('bruno.martins','123','USER','salt08',8),
-- ('patricia.alves','123','USER','salt09',9),
-- ('rafael.gomes','123','USER','salt10',10),
-- ('juliana.ribeiro','123','USER','salt11',11),
-- ('daniel.santos','123','USER','salt12',12),
-- ('camila.torres','123','USER','salt13',13),
-- ('eduardo.nunes','123','USER','salt14',14),
-- ('renata.freitas','123','USER','salt15',15),
-- ('thiago.barros','123','USER','salt16',16),
-- ('aline.pacheco','123','USER','salt17',17),
-- ('marcos.teixeira','123','USER','salt18',18),
-- ('bianca.lopes','123','USER','salt19',19),
-- ('felipe.araujo','123','USER','salt20',20),
-- ('larissa.mendes','123','USER','salt21',21);


-- INSERT INTO tbProdutos(codProd,nome,quantidade,peso,unidade,codBar,dataDeEntrada,dataDeValidade,dataLimiteDeSaida,codUsu)VALUES(1,'Arroz Branco',10,5,'KG','1234561234561','2025-09-16','2026-09-10','2026-07-30',1);

-- SELECT nome AS nomeProduto, SUM(quantidade) AS totalQuantidadeProdutos, FROM tbProdutos GROUP BY nome ORDER BY totalQuantidadeProdutos DESC, totalQuantidadeEstoque DESC LIMIT 8;

-- SELECT nome, SUM(quantidade) FROM tbProdutos WHERE codProd = 1;

-- SELECT nome AS nomeProduto, SUM(quantidade) FROM tbProdutos GROUP BY nome;

-- SELECT nome AS nomeProduto, SUM(quantidade) AS totalQuantidadeProdutos FROM tbProdutos GROUP BY nome ORDER BY totalQuantidadeProdutos DESC LIMIT 8;

-- INSERT INTO tbProdutos(codProd,nome,quantidade,peso,unidade,codBar,dataDeEntrada,dataDeValidade,dataLimiteDeSaida,codUsu)VALUES(2,'Feijão Carioca',5,1,'KG','1234561444888','2025-09-10','2026-09-05','2026-02-15',1);

-- SELECT p.nome AS nomeProduto, SUM(p.quantidade) AS totalQuantidadeProdutos FROM tbProdutos as p GROUP BY p.nome ORDER BY totalQuantidadeProdutos DESC LIMIT 8;

-- INSERT INTO tbProdutos(codProd,nome,quantidade,peso,unidade,codBar,dataDeEntrada,dataDeValidade,dataLimiteDeSaida,codUsu)VALUES(3,'Macarrão',20,500,'G','1234561555333','2025-06-10','2025-12-25','2026-03-05',1);

-- INSERT INTO tbProdutos(codProd,nome,quantidade,peso,unidade,codBar,dataDeEntrada,dataDeValidade,dataLimiteDeSaida,codUsu)VALUES(4,'Farinha de trigo',7,1,'KG','5468761566644','2025-09-11','2025-11-30','2026-12-28',1);