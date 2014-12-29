package ch.fhnw.lederer.virtualmachineHS2010;

/* Dr. Edgar Lederer, Fachhochschule Nordwestschweiz */

public class TestVM {
    static final int CODE_SIZE= 1000;
    static final int STORE_SIZE= 1000;
    static IVirtualMachine vm= new VirtualMachine(CODE_SIZE, STORE_SIZE);

/*
    program intDiv
    global
      proc divide(in copy m:int, in copy n:int, out ref q:int, out ref r:int)
      do
        q init := 0;
        r init := m;
        while r >= n do
          q := q + 1;
          r := r - n
        endwhile
      endproc;

      var m:int;
      var n:int;
      var q:int;
      var r:int
    do
      ? m init;
      ? n init;
      call divide(m, n, q init, r init);
      ! q;
      ! r
    endprogram
*/

    public static void main(String[] args) {
        try {
            vm.Alloc(0, 4);
            vm.IntLoad(1, 0);
            vm.IntInput(2, "m");
            vm.IntLoad(3, 1);
            vm.IntInput(4, "n");
            vm.Alloc(5, 0);
            vm.IntLoad(6, 0);
            vm.Deref(7);
            vm.IntLoad(8, 1);
            vm.Deref(9);
            vm.IntLoad(10, 2);
            vm.IntLoad(11, 3);
            vm.Call(12, 20);
            vm.IntLoad(13, 2);
            vm.Deref(14);
            vm.IntOutput(15, "q");
            vm.IntLoad(16, 3);
            vm.Deref(17);
            vm.IntOutput(18, "r");
            vm.Stop(19);
            vm.Enter(20, 0, 0);
            vm.IntLoad(21, 0);
            vm.LoadRel(22, (-2));
            vm.Deref(23);
            vm.Store(24);
            vm.LoadRel(25, (-4));
            vm.Deref(26);
            vm.LoadRel(27, (-1));
            vm.Deref(28);
            vm.Store(29);
            vm.LoadRel(30, (-1));
            vm.Deref(31);
            vm.Deref(32);
            vm.LoadRel(33, (-3));
            vm.Deref(34);
            vm.IntGE(35);
            vm.CondJump(36, 55);
            vm.LoadRel(37, (-2));
            vm.Deref(38);
            vm.Deref(39);
            vm.IntLoad(40, 1);
            vm.IntAdd(41);
            vm.LoadRel(42, (-2));
            vm.Deref(43);
            vm.Store(44);
            vm.LoadRel(45, (-1));
            vm.Deref(46);
            vm.Deref(47);
            vm.LoadRel(48, (-3));
            vm.Deref(49);
            vm.IntSub(50);
            vm.LoadRel(51, (-1));
            vm.Deref(52);
            vm.Store(53);
            vm.UncondJump(54, 30);
            vm.Return(55, 4);
        } catch (IVirtualMachine.CodeTooSmallError e) {
            System.out.println(e.toString());
            return;
        }

        try {
            vm.execute();
        } catch (IVirtualMachine.ExecutionError e) {
            System.out.println(e);
            return;
        }
    }
}
