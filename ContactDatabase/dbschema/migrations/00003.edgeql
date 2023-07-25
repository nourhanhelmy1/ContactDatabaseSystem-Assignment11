CREATE MIGRATION m1h6dg6iryy2jeq3lo5cz6lce3iwyuvdmqm2cijprbx5xulgkazlfq
    ONTO m153q3jypktpi7fzosncaydfodptpsbzz6ftiuj4pcngbqbxrzbczq
{
  ALTER TYPE default::Contact {
      CREATE REQUIRED PROPERTY role: std::str {
          SET REQUIRED USING (<std::str>{});
      };
  };
};
