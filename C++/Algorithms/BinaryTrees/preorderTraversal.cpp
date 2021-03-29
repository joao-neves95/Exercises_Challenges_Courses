/**
 * @file preorderTraversal.cpp
 * @author João Neves (https://github.com/joao-neves95)
 * @brief
 Given the root of a binary tree, return the preorder traversal of its nodes values.
 (Traverse left subtree first and then the right subtree)

 * @version 1.0.0
 * @copyright Copyright (c) 2021
 * (_SHIVAYL_)
 */
#include <iostream>
#include <vector>
#include <stack>
using namespace std;

/**
 * Definition for a binary tree node.
 */
struct TreeNode {
    int val;
    TreeNode *left;
    TreeNode *right;
    TreeNode() : val(0), left(nullptr), right(nullptr) {}
    TreeNode(int x) : val(x), left(nullptr), right(nullptr) {}
    TreeNode(int x, TreeNode *left, TreeNode *right) : val(x), left(left), right(right) {}
};

class Solution {
public:
    vector<int> preorderTraversalIterative(TreeNode* root) {
        // Visual notes:
        // [root, left1, right1, left2, right2]
        //
        // [1,null,2,3]
        //
        // root(1) -----> right1(2) ---> right2(null)
        //         `-> left1(null)  `--> left2(3)
        //
        // [3,1,2,null,4]
        // root(3) ----> right1(2)
        //         `-> left1(1) ---> right2(4)
        //                      `--> left2(null)
        //
        vector<int> response;
        stack<TreeNode*> nodeStack;
        nodeStack.push(root);
        TreeNode* thisNode;

        while (!nodeStack.empty()) {
            thisNode = nodeStack.top();
            response.push_back(thisNode->val);
            nodeStack.pop();

            this->pushStackIfNotNull(&nodeStack, thisNode->right);
            // We put left at the top (LIFO) to visit every left node of each sub-tree first.
            this->pushStackIfNotNull(&nodeStack, thisNode->left);
        }

        return response;
    }

    void pushStackIfNotNull(stack<TreeNode*>* stack, TreeNode* node) {
        if (node != nullptr) {
            stack->push(node);
        }
    }
};

int main() {
    return 0;
}
