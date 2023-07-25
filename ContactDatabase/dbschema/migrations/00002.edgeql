CREATE MIGRATION m153q3jypktpi7fzosncaydfodptpsbzz6ftiuj4pcngbqbxrzbczq
    ONTO m1q6lx3qsbz4ldw5gqa4ct33wbplujixhr7jz2posveowvl4yiazzq
{
  ALTER TYPE default::Contact {
      CREATE REQUIRED PROPERTY password: std::str {
          SET REQUIRED USING (<std::str>{});
      };
      CREATE REQUIRED PROPERTY username: std::str {
          SET REQUIRED USING (<std::str>{});
      };
  };
};
