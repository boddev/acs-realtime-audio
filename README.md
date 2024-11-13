# acs-realtime-audio

#+title: Week 1 Lecture
#+HTML_HEAD_EXTRA: <style> pre.src { background-color: #252525; color: white }</style>
#+options: \n:t
#+OPTIONS: html-postamble:nil
#+OPTIONS: num:nil
#+PROPERTY: header-args :numbers yes

* Tree Terminology
- A *root* is a node with no ancestors.
- A *leaf* is a node with no descendants.
  - Note that in a singleton tree (tree with one node), the root is also a leaf.
- You may hear different terms for the relationship between nodes: ancestors and descendants, parents and children, node siblings, etc. The terminology is less important than understanding the relationship between nodes.
- An invariant of the tree structure in computer science is that no node will have two parents. Broadly speaking you can say that a tree will never have a cycle.
- The *size* of a node is the number descendants (including itself). A leaf node will have a size of 1. The size of a tree is the size of its root. Empty tree has size of 0.
- The *height* of a node is the number of edges on the longest path from the node to a leaf. A leaf node will have a height of 0. The height of tree is the height of its root. Empty tree has height of -1.
- The *depth* of a node is the number of edges from the tree's root to the node. A root node will have a depth of 0. The depth of a tree is the same as it's height.

[[./images/tree-height-depth.png]]

* Tree Traversals

[[./images/tree-simple.png]]
- Level-order: 41 21 61 11 31
  - Level order means processing each level (or depth) from left to right
- In-order: (11 21 31) 41 61
  - In-order means recursively processing the left children, the current node, then the right children
- Pre-order: 41 (21 11 31) 61
  - Pre-order means recursively processing the current node, the left children, then the right children
- Post-order: (11 31 21) 61 41
  - Post-order means recursively processing the left children, the right children, then current node
Note: Parentheses are included only for clarity.

=========================================================================================================================

* Tree code: While loop going left
#+begin_src java
    public int sizeLeft() {
        Node current = root;
        int sz = 0;
        while (current != null) {
            current = current.left;
            sz = sz + 1;
        }
        return sz;
    }
#+end_src
While loop going left.

[[../extras/traces/sizeLeftIterative1.pdf][Here is a trace of an execution.]]

Back to the size problem. Here is starter code which computes the size of the leftmost branch in the tree.

* Tree code: While loop going left
A variant.
#+begin_src java
    public int sizeLeft() {
        Node current = root;
        int sz = 0;
        while (true) {
            if (current == null) break;
            current = current.left;
            sz = sz + 1;
        }
        return sz;
    }
#+end_src
* Tree code: While loop going left
Another variant.
#+begin_src java
    public int sizeLeft() {
        Node current = root;
        int sz = 0;
        while (true) {
            if (current == null) return sz;
            current = current.left;
            sz = sz + 1;
        }
    }
#+end_src

=========================================================================================================================

* Recursion refresher

If it's been a while since you've used recursion here are a couple refresher points. Feel free to skip if you're comfortable with recursion.
- Any problem that can be solved with recursion can also be solved with a stack, but generally you use recursion because it makes your code much cleaner in certain cases.
- Before writing any recursive code, try to define your problem recursively.
  - In this case, we can define a tree as one of two things:
    1. An empty tree
    2. A node with children who are also trees
- Notice how those two possible definitions correspond to our two execution cases for writing recursive functions: the *base case* and the *recursive case* respectively. Remember that one of the goals of using recursion is to break a large problem (i.e. the recursive case) down into small enough chunks that it can be solved directly (i.e. the base case).
- When writing recursive code, I often use this 'magic recipe' to help me get started.
  1. Create private helper function with same name and at least one parameter
  2. In the body of the helper function, write code for two cases:
     1. Base case where you can solve the problem directly (here if the tree is empty (null))
     2. Recursive case - make at least one recursive call to process the rest of the data structure but *don't think about it yet.* If the recursive call returns an answer, store it in a variable called ~temp~ or similar. ~temp~ almost always has same type as the return type of the function.
  3. Start thinking about what the recursive case actually does. Mentally substitute the recursive call with what it SHOULD return. Think about how the value at the root of the tree should compare with that result (should it come before it, or after it, be added to it or subtracted from it, etc).
  4. If needed, draw a picture of a hypothetical tree (i.e. root and two subtrees as triangles). Fill in hypothetical return values in those subtrees, then ask what to do with them to get the correct answer.

=========================================================================================================================

* Tree code: Recursion going left
Recursion going left

#+begin_src java
    public int sizeLeft() {
        return sizeLeft(root, 0);
    }
    private static int sizeLeft(Node current, int sz) {
        if (current != null) {
            sz = sizeLeft(current.left, sz + 1);
        }
        return sz;
    }
#+end_src

[[../extras/traces/sizeLeft1.pdf][Here is a trace of an execution.]]

* Tree code: Recursion going left and right
Recursion going left and right, now computing the size of the tree.

Is this correct?

#+begin_src java
    public int size() {
        return size(root, 0);
    }
    private static int size(Node current, int sz) {
        if (current != null) {
            sz = size(current.left, sz + 1);
            sz = size(current.right, sz + 1);
        }
        return sz;
    }
#+end_src

[[../extras/traces/sizeBroken1.pdf][Here is a trace of an execution.]]

* Tree code: Recursion going left and right
Adding to the size only once.

#+begin_src java
    public int size() {
        return size(root, 0);
    }
    private static int size(Node current, int sz) {
        if (current != null) {
            sz = sz + 1;
            sz = size(current.left, sz);
            sz = size(current.right, sz);
        }
        return sz;
    }
#+end_src

[[../extras/traces/sizeThreaded1.pdf][Here is a trace of an execution.]]

What is the initial value of =sz= at each node as we go around the tree?

Computation of the size happens as we go /forward/, in a preorder traversal.

Initial value of =sz= at each node is the number of nodes that precede this one in a preorder traversal. The initial value of =sz= does not include the =current= node. The return value of =sz= is the initial value plus the size of the tree rooted at =current=.

=========================================================================================================================

* Tree code: Base case first (negating the conditional)

#+begin_src java
    public int size() {
        return size(root, 0);
    }
    private static int size(Node current, int sz) {
        if (current == null) return sz;
        sz = sz + 1;
        sz = size(current.left, sz);
        sz = size(current.right, sz);
        return sz;
    }
#+end_src

Here the base case comes first and returns immediately. Note that the base case now checks for null (instead of checking for non-null). This is more idiomatic for recursive functions.

* Tree code: Is this correct?
Is this correct?

#+begin_src java
    public int size() {
        return size(root, 0);
    }
    private static int size(Node current, int sz) {
        if (current == null) return sz;
        sz += 1;
        sz += size(current.left, sz);
        sz += size(current.right, sz);
        return sz;
    }
#+end_src

No. The return value of =size(current, sz)= includes the current node. So by using ~+=~ for both of the recursive calls, we're overcounting the current node. Mentally walk through what would happen if we passed in a tree that was a single node.

[[../extras/traces/sizeBroken2.pdf][Here is a trace of an execution.]]

* Tree code: Is this correct?
What about this?

#+begin_src java
    public int size() {
        return size(root, 0);
    }
    private static int size(Node current, int sz) {
        if (current == null) return sz;
        sz = sz + 1;
        size(current.left, sz);
        size(current.right, sz);
        return sz;
    }
#+end_src

No. Mistake here is to think of =sz= as shared among the function calls.

[[../extras/traces/sizeBroken3.pdf][Here is a trace of an execution.]]

* Tree code: Back to sanity!

#+begin_src java
    public int size() {
        return size(root, 0);
    }
    private static int size(Node current, int sz) {
        if (current == null) return sz;
        sz = size(current.left, sz);
        sz = size(current.right, sz);
        sz += 1;
        return sz;
    }
#+end_src

Threaded parameter: correct version, doing the addition postorder. A *threaded parameter* refers to /threading/ a value through a series of recursive calls. In this case, we /thread/ the =sz= parameter through all of our recursive calls before using it. It has nothing to do with multithreaded code.

What are the initial values of =sz= as you go around the tree?

=========================================================================================================================

* Tree code: Make right call independent of the left

#+begin_src java
    public int size() {
        return size(root, 0);
    }
    private static int size(Node current, int sz) {
        if (current == null) return sz;
        sz = size(current.left, 0);
        sz += size(current.right, 0);
        sz += 1;
        return sz;
    }
#+end_src

Make it so that the right does not know about the left!

Change so that size returns just the size of =current=, rather than size of =current= plus =sz=

Intial value of =sz= is always 0

* Tree code: Don't need the sz parameter!

#+begin_src java
    public int size() {
        return size(root);
    }
    private static int size(Node current) {
        if (current == null) return 0;
        int sz = 0;
        sz += 1;
        sz += size(current.left);
        sz += size(current.right);
        return sz;
    }
#+end_src

Don't need the sz parameter!

Node =current= was counted postorder, now it's preorder. With the current approach, it doesn't matter where we put ~sz += 1~, since the intermediate values are not passed around.

* Tree code: Local variables don't matter

#+begin_src java
    public int size() {
        return size(root);
    }
    private static int size(Node current) {
        if (current == null) return 0;
        int szl = size(current.left);
        int szr = size(current.right);
        return szl + szr + 1;
    }
#+end_src

Local variables don't matter.

[[../extras/traces/sizeCanonical2.pdf][Here is a trace of an execution.]]

Compare the above snippet with this one:

#+begin_src java
    public int size() {
        return size(root);
    }
    private static int size(Node current) {
        if (current == null) return 0;
        return size(current.left) + size(current.right) + 1;
    }
#+end_src

From a compiler point of view, this code is identical to the previous version. From a readability and maintainability perspective, which one is clearer?

=========================================================================================================================

* Tree code: Nullable
Back to the version with one variable.

#+begin_src java
    public int size() {
        return size(root);
    }
    private static int size(Node current) {
        if (current == null) return 0;
        int sz = 1;
        sz += size(current.left);
        sz += size(current.right);
        return sz;
    }
#+end_src

Note that it does not matter when we add =1= to =sz=, since we don't carry the intermediate values around as parameters.

In this code, we make the call, then check for null. The variable =current= is /nullable/ (may be assigned =null=). Lets call this the "nullable" version.

We might also call it "optimistic", or "Just do it!"

* Tree code: Non-nullable

#+begin_src java
    public int size() {
        if (root == null) return 0;
        return size(root);
    }
    private static int size(Node current) {
        int sz = 1;
        if (current.left != null) sz += size(current.left);
        if (current.right != null) sz += size(current.right);
        return sz;
    }
#+end_src

In this code, we check for null, then make the call. The variable =current= is /non-nullable/ (must not be assigned =null=). Lets call this the "non-nullable" version.

We might also call this "cautious", or "Look before you leap!"

* Tree code: The winner is...
#+begin_export html
<table>
  <tr>
    <td>
#+end_export

#+begin_src java
    public int size() {
        return size(root);
    }
    private static int size(Node current) {
        if (current == null) return 0;
        int sz = 1;
        sz += size(current.left);
        sz += size(current.right);
        return sz;
    }
#+end_src

#+begin_export html
    </td>
    <td>
#+end_export

#+begin_src java
    public int size() {
        if (root == null) return 0;
        return size(root);
    }
    private static int size(Node current) {
        int sz = 1;
        if (current.left != null) sz += size(current.left);
        if (current.right != null) sz += size(current.right);
        return sz;
    }
#+end_src

#+begin_export html
    </td>
  </tr>
</table>
#+end_export

What are the advantages of each approach?

In general, which should you prefer?

Nullable has one /static/ conditional (=if= statement). Non-nullable has three! However, we have exactly the same number of /dynamic/ conditionals (executions of the =if= statement).

Nullable has about twice as many dynamic function calls! Count them! But "Nullable" version has less redundancy in the code itself, which generally makes it easier to maintain. This is a huge win. "Non-nullable" version is also known as "Too many base cases!" because you're performing the same check (is something null) in multiple places. For these reasons, you'd generally prefer the Nullable version in a professional setting.

Only prefer performance over maintainability if you have determined that the performance is an actual problem and you have done profiling to determine the actual location of the problem.

*Software engineering adage*: "Code is written once but read countless times." In other words, prefer maintainability and readability in your code unless you have a good reason to prioritize something else.

One of the trickiest things for Java programmers is to keep track of when a variable is nullable. If you haven't heard of it, adding "null" to the Algol programming language has been called the "Billion Dollar Mistake" by Tony Hoare.

Newer languages such as [[https://developer.apple.com/documentation/swift/optional][swift]] and [[https://kotlinlang.org/docs/reference/null-safety.html][kotlin]] distinguish nullable and non-nullable types. In these languages, a variable that may be null must be given a type that ends in a question mark. (In swift, =null= is written =nil=, or equivalently as =Optional.none=.) In these languages, we would give different types to the argument in the helper function above. For the nullable version, =current= would be given type =Node?= whereas for the non-nullable version, it would be given type =Node=.

=========================================================================================================================

* Tree code: Negligent!
What's wrong with this code?

#+begin_src java
    public int size() {
        return size(root);
    }
    private static int size(Node current) {
        int sz = 1;
        if (current.left != null) sz += size(current.left);
        if (current.right != null) sz += size(current.right);
        return sz;
    }
#+end_src

If the node is =null=, we still return a size of =1= which is incorrect. Think about an empty tree or the checking the children of a leaf node.

* Tree code: Paranoid!
What's wrong with this code?

#+begin_src java
    public int size() {
        if (root == null) return 0;
        return size(root);
    }
    private static int size(Node current) {
        if (current == null) return 0;
        int sz = 1;
        if (current.left != null) sz += size(current.left);
        if (current.right != null) sz += size(current.right);
        return sz;
    }
#+end_src

We perform the same check multiple times, leading to wasted computation.

* Tree code: Returns too quickly!
What's wrong with this code?

#+begin_src java
    public int size() {
        if (root == null) return 0;
        return size(root);
    }
    private static int size(Node current) {
        if (current.left != null) return 1 + size(current.left);
        if (current.right != null) return 1 + size(current.right);
        return 1;
    }
#+end_src

If we try to use =return= statements in our control flow, we will never hit the right branches of any nodes.

* Tree code: Threaded parameter with non-nullable pointer 0
Threaded parameters with non-nullable pointer.
#+begin_export html
<div style="display: flex; justify-content: space-between;">
  <div style="width: 55%;">
#+end_export
#+begin_src java
public int size() {
    if (root == null) return 0;
    return size(root, 0);
}
private static int size(Node current, int sz) {
    sz = sz + 1;
    if (current.left != null) sz = size(current.left, sz);
    if (current.right != null) sz = size(current.right, sz);
    return sz;
}
#+end_src
#+begin_export html
  </div>
  <div style="width: 58%;">
#+end_export

#+begin_src java
public int size() {
    if (root == null) return 0;
    return size(root, 1);
}
private static int size(Node current, int sz) {
    if (current.left != null) sz = size(current.left, sz + 1);
    if (current.right != null) sz = size(current.right, sz + 1);
    return sz;
}
#+end_src
#+begin_export html
  </div>
</div>
#+end_export

Describe the difference from the previous version. Is either correct?

Both are correct, but they differ in how they count. The left version counts the /current/ node then passes that information to the children. The right version counts the /children/ themselves, then passes that information to the recursive call. In other words, at the start of the recursive calls on the left, the count reflects everything visited so far excluding the current node. At the start of the recursive calls on the right, the count reflects everything visited so far including the current node.

=========================================================================================================================

* Tree code: Deriving non-nullable code from a loop
Let's bring this back to where we started by looking at a variant of the loop =sizeLeft=. Recall the original code. Here, you count the node referenced by =current=.

#+begin_src java
    public int sizeLeft() {
        Node current = root;
        int sz = 0;
        while (current != null) {
            current = current.left;
            sz = sz + 1;
        }
        return sz;
    }
#+end_src

In the following variant, we assume that =current= is already taken care of, and you work on =current.left= instead.

#+begin_src java
    public int sizeLeft() {
        if (root == null) return 0;
        Node current = root;
        int sz = 1;
        while (current.left != null) {
            current = current.left;
            sz = sz + 1;
        }
        return sz;
    }
#+end_src

Note that =current= is non-nullable. It "hangs back" one place. We can make that loop recursive:

#+begin_src java
    public int sizeLeft() {
        if (root == null) return 0;
        return sizeLeft(root, 1);
    }
    private static int sizeLeft(Node current, int sz) {
        if (current.left != null) {
            sz = sizeLeft(current.left, sz + 1);
        }
        return sz;
    }
#+end_src

Then if we add code to handle the right child as well, we get back to the definition we had earlier.

#+begin_src java
    public int size() {
        if (root == null) return 0;
        return size(root, 1);
    }
    private static int size(Node current, int sz) {
        if (current.left != null) sz = size(current.left, sz + 1);
        if (current.right != null) sz = size(current.right, sz + 1);
        return sz;
    }
#+end_src

=========================================================================================================================
